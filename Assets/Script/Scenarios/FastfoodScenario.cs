using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastfoodScenario : Scenario
{
    public GameObject fastfoodProps;

    private AgentsController agentsController;

    private Dictionary<string, string> fastfoodStateDictionary;

    private TextOCEAN textOCEAN;
    private string tmpStr;

    private int currentStateNo;

    private int productId = -1;
    private int menuId = -1;

    private float totalPrice = 0f;

    private bool askedIfLarge = false;
    
    private readonly string burgerListStr = "Cheese Burger, Meat Burger, Twin Burger and Chicken Burger";
    private readonly string drinkListStr = "Coke, Fanta and Sprite";
    private readonly string sideListStr = "Chips and Salad";

    private readonly string[] productNames =
    {
        "Cheese Burger",
        "Meat Burger",
        "Twin Burger",
        "Chicken Burger"
    };

    private readonly string[] menuNames =
    {
        "Cheese Burger Menu",
        "Meat Burger Menu",
    };

    private readonly float[] menuPrices =
    {
        9.50f,
        13,99f
    };

    private readonly float[] singlePrices =
    {
        7.99f,
        9.99f,
        12.99f,
        6.99f,
        2.00f,
        2.00f,
        2.00f,
        3.50f,
        3.00f
    };

    enum CurrentlyOrdering { BURGER, SIDE, DRINK };
    private CurrentlyOrdering currentlyOrdering;

    private void Start()
    {
        InitFastFoodStateDictionary();
    }

    public override AgentResponse DecodeResponse(EntityIntent ei)
    {
        // get textOCEAN from agent
        textOCEAN = agentsController.GetCurrentAgent().DetermineTextOCEAN();

        AgentResponse agentResponse = new AgentResponse("EMPTY");
        agentResponse.responseTextOCEAN = textOCEAN;

        print("decoding: " + ei.Intent + " * " + textOCEAN);

        switch (ei.Intent)
        {
            case "ProductList": // 01
                currentStateNo = 1;
                if(currentlyOrdering == CurrentlyOrdering.BURGER)
                {
                    agentResponse.responseText = GetFastFoodText(textOCEAN, 2);
                }
                else if (currentlyOrdering == CurrentlyOrdering.DRINK)
                {
                    agentResponse.responseText = GetFastFoodText(textOCEAN, 3);
                }
                else if (currentlyOrdering == CurrentlyOrdering.SIDE)
                {
                    agentResponse.responseText = GetFastFoodText(textOCEAN, 4);
                }
                break;
            case "BurgerList": // 02
                currentStateNo = 2;
                agentResponse.responseText = GetFastFoodText(textOCEAN, 2);
                break;
            case "DrinkList": // 03
                currentStateNo = 3;
                agentResponse.responseText = GetFastFoodText(textOCEAN, 3);
                break;
            case "SideList": // 04
                currentStateNo = 4;
                agentResponse.responseText = GetFastFoodText(textOCEAN, 4);
                break;
            case "WantProduct": // 05 - 06 - 07
                productId = GetProductId(ei.Entity);
                if (productId != -1)
                {
                    if(currentStateNo == 9)
                    {
                        currentStateNo = 9;
                        agentResponse.responseText = GetFastFoodText(textOCEAN, 10);
                        agentResponse.responseText = agentResponse.responseText.Replace("%product", "" + productNames[productId]);
                    }
                    else
                    {
                        if(productId == 0 || productId == 1 || productId == 2 || productId == 3)
                        {
                            // this is a burger
                            // check if it is in menu
                            menuId = CheckProducInMenu(productId);
                            if (menuId != -1)
                            {
                                currentStateNo = 5;
                                agentResponse.responseText = GetFastFoodText(textOCEAN, 5);
                                agentResponse.responseText = agentResponse.responseText.Replace("%menu", menuNames[menuId]);
                                agentResponse.responseText = agentResponse.responseText.Replace("%price", "" + menuPrices[menuId]);
                            }
                            else
                            {
                                currentStateNo = 6;
                                AddProductToOrder(productId, false); // it is not in menu, so add it
                                agentResponse.responseText = GetFastFoodText(textOCEAN, 6);
                                currentlyOrdering = CurrentlyOrdering.DRINK;
                            }
                        }
                        else if (productId == 4 || productId == 5 || productId == 6)
                        {
                            // this is a drink
                                
                            askedIfLarge = true;
                            agentResponse.responseText = GetFastFoodText(textOCEAN, 8);

                            currentlyOrdering = CurrentlyOrdering.SIDE;
                        }
                        else
                        {
                            // this ia a side meal

                            currentlyOrdering = CurrentlyOrdering.BURGER;
                        }
                    }
                }
                else
                {
                    // we do not sell this.
                    currentStateNo = 7;
                    agentResponse.responseText = GetFastFoodText(textOCEAN, 7);
                    agentResponse.responseText = agentResponse.responseText.Replace("%product", ei.Entity);
                }
                break;
            case "Yes":
                if(currentStateNo == 5)
                {
                    currentStateNo = 8;
                    agentResponse.responseText = GetFastFoodText(textOCEAN, 8);
                    agentResponse.responseText = agentResponse.responseText.Replace("%price", "" + (menuPrices[menuId]+2f));
                }
                else if(currentStateNo == 8) // make selection large!
                {
                    currentStateNo = 9;
                    agentResponse.responseText = GetFastFoodText(textOCEAN, 9);
                    AddMenuToOrder(menuId, true);
                }
                else
                {
                    agentResponse.responseText = "Yes to what?";
                }
                break;
            case "No":
                if (currentStateNo == 8)
                {
                    currentStateNo = 9;
                    agentResponse.responseText = GetFastFoodText(textOCEAN, 9);
                    AddMenuToOrder(menuId, false);
                }
                else if (currentStateNo == 9)
                {
                    currentStateNo = 11;
                    agentResponse.responseText = GetFastFoodText(textOCEAN, 11);
                    agentResponse.responseText = agentResponse.responseText.Replace("%price", "" + totalPrice);
                }
                break;
            case "CreditCard":
                // play credit animation
                currentStateNo = 12;
                agentResponse.responseText = GetFastFoodText(textOCEAN, 12);
                break;
            case "Cash":
                // play cash animation
                currentStateNo = 12;
                agentResponse.responseText = GetFastFoodText(textOCEAN, 12);
                break;
            default:
                agentResponse.responseText = "ERROR! " + ei.Intent;
                break;
        }

        return agentResponse;
    }

    private int GetProductId(string str)
    {
        if(str.Equals("cheese burger", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 0;
        }
        else if(str.Equals("meat burger", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 1;
        }
        else if (str.Equals("twin burger", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 2;
        }
        else if (str.Equals("chicken burger", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 3;
        }
        else if (str.Equals("coke", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 4;
        }
        else if (str.Equals("sprite", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 5;
        }
        else if (str.Equals("fanta", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 6;
        }
        else if (str.Equals("chips", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 7;
        }
        else if (str.Equals("salad", System.StringComparison.InvariantCultureIgnoreCase))
        {
            return 8;
        }
        else
        {
            return -1;
        }
    }

    private int CheckProducInMenu(int id)
    {
        if (id == 0)
        {
            return 0;
        }
        else if (id == 1)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private void AddProductToOrder(int id, bool isLarge)
    {
        // add product id to basket!!
        totalPrice += singlePrices[id];
        if(isLarge) totalPrice += 1.99f;
    }

    private void AddMenuToOrder(int id, bool isLarge)
    {
        // add menu items to basket!!
        totalPrice += menuPrices[id];
        if (isLarge) totalPrice += 1.99f;
    }

    public override void Init(MainLogic mainLogic)
    {
        currentlyOrdering = CurrentlyOrdering.BURGER;
        fastfoodProps.SetActive(true);

        this.mainLogic = mainLogic;
        agentsController = mainLogic.GetAgentsController();

        agentsController.ActivateNextAgent();

        mainLogic.StartSpeechRecognition();
    }

    public override void DeInit()
    {
        fastfoodProps.SetActive(false);
    }

    public override bool IsOnEndState()
    {
        return false;
    }

    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }

    private static string GetDictionaryString(TextOCEAN ocean, int state)
    {
        return ocean.ToString() + state;
    }

    private string GetFastFoodText(TextOCEAN ocean, int state)
    {
        tmpStr = "Something went wrong!";
        fastfoodStateDictionary.TryGetValue(GetDictionaryString(ocean, state), out tmpStr);
        return tmpStr;
    }

    public void InitFastFoodStateDictionary()
    {
        fastfoodStateDictionary = new Dictionary<string, string>
        {
            // state 1
            { GetDictionaryString(TextOCEAN.O_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.O_neg, 1), ""},
            { GetDictionaryString(TextOCEAN.C_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.C_neg, 1), ""},
            { GetDictionaryString(TextOCEAN.E_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.E_neg, 1), ""},
            { GetDictionaryString(TextOCEAN.A_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.A_neg, 1), ""},
            { GetDictionaryString(TextOCEAN.N_pos, 1), ""},
            { GetDictionaryString(TextOCEAN.N_neg, 1), ""},

            // state 2
            { GetDictionaryString(TextOCEAN.O_pos, 2), "Our delightful restaurant has " + burgerListStr + ", which one could I prepare for you?" },
            { GetDictionaryString(TextOCEAN.O_neg, 2), "We have " + burgerListStr + "."},
            { GetDictionaryString(TextOCEAN.C_pos, 2), "We do sell " +  burgerListStr + "."},
            { GetDictionaryString(TextOCEAN.C_neg, 2), "Oh, well... We got burgers. We have " + burgerListStr + "."},
            { GetDictionaryString(TextOCEAN.E_pos, 2), "My friend, we sell " + burgerListStr + ". Which one would you like?"},
            { GetDictionaryString(TextOCEAN.E_neg, 2), burgerListStr + "." },
            { GetDictionaryString(TextOCEAN.A_pos, 2), "We have " + burgerListStr + ". Which one would you like?" },
            { GetDictionaryString(TextOCEAN.A_neg, 2), "Ok, if you don't know, we sell " + burgerListStr + "."},
            { GetDictionaryString(TextOCEAN.N_pos, 2), "Oh, well, we have fries... Oh, you mean burgers, right? Then it is " + burgerListStr + "."},
            { GetDictionaryString(TextOCEAN.N_neg, 2), "We have " + burgerListStr + ". Which one would you like?"},

             // state 3
            { GetDictionaryString(TextOCEAN.O_pos, 3), "Our delightful restaurant has " + drinkListStr + ", which one could I prepare for you?" },
            { GetDictionaryString(TextOCEAN.O_neg, 3), "We have " + drinkListStr + "."},
            { GetDictionaryString(TextOCEAN.C_pos, 3), "We do sell " +  drinkListStr + "."},
            { GetDictionaryString(TextOCEAN.C_neg, 3), "Oh, well... We got drinks. We have " + drinkListStr + "."},
            { GetDictionaryString(TextOCEAN.E_pos, 3), "My friend, we sell " + drinkListStr + ". Which one would you like?"},
            { GetDictionaryString(TextOCEAN.E_neg, 3), drinkListStr + "." },
            { GetDictionaryString(TextOCEAN.A_pos, 3), "We have " + drinkListStr + ". Which one would you like?" },
            { GetDictionaryString(TextOCEAN.A_neg, 3), "Ok, if you don't know, we sell " + drinkListStr + "."},
            { GetDictionaryString(TextOCEAN.N_pos, 3), "Oh, well, we have burgers... Oh, you mean drinks, right? Then it is " + drinkListStr + "."},
            { GetDictionaryString(TextOCEAN.N_neg, 3), "We have " + drinkListStr + ". Which one would you like?"},

            // state 4
            { GetDictionaryString(TextOCEAN.O_pos, 4), "Our delightful restaurant has " + sideListStr + ", which one could I prepare for you?" },
            { GetDictionaryString(TextOCEAN.O_neg, 4), "We have " + sideListStr + "."},
            { GetDictionaryString(TextOCEAN.C_pos, 4), "We do sell " +  sideListStr + "."},
            { GetDictionaryString(TextOCEAN.C_neg, 4), "Oh, well... We got side meals. We have " + sideListStr + "."},
            { GetDictionaryString(TextOCEAN.E_pos, 4), "My friend, we sell " + sideListStr + ". Which one would you like?"},
            { GetDictionaryString(TextOCEAN.E_neg, 4), sideListStr + "." },
            { GetDictionaryString(TextOCEAN.A_pos, 4), "We have " + sideListStr + ". Which one would you like?" },
            { GetDictionaryString(TextOCEAN.A_neg, 4), "Ok, if you don't know, we sell " + sideListStr + "."},
            { GetDictionaryString(TextOCEAN.N_pos, 4), "Oh, well, we have burgers... Oh, you mean side meals, right? Then it is " + sideListStr + "."},
            { GetDictionaryString(TextOCEAN.N_neg, 4), "We have " + sideListStr + ". Which one would you like?"},

            // state 5
            { GetDictionaryString(TextOCEAN.O_pos, 5), "Of course, also it is possible to get %menu for only %price, would you like to do so?" },
            { GetDictionaryString(TextOCEAN.O_neg, 5), "Want to make it %menu for %price?"},
            { GetDictionaryString(TextOCEAN.C_pos, 5), "Ok, would you like to make it %menu for %price?"},
            { GetDictionaryString(TextOCEAN.C_neg, 5), "Sure, ok... Hmmm, we also have %menu for %price, do you want to get %menu?"},
            { GetDictionaryString(TextOCEAN.E_pos, 5), "Sure my friend, I could also get you %menu, it is only %price, how about that?"},
            { GetDictionaryString(TextOCEAN.E_neg, 5), "There is %menu for %price. Do you want to get the menu?" },
            { GetDictionaryString(TextOCEAN.A_pos, 5), "Sure, we also have %menu for %price, would you like the menu?" },
            { GetDictionaryString(TextOCEAN.A_neg, 5), "Well, we also have a menu, you know? Want to make it %menu for %price?"},
            { GetDictionaryString(TextOCEAN.N_pos, 5), "Oh, ok... By the way, we can... We could make it a menu, you know. There is %menu, it is only %price."},
            { GetDictionaryString(TextOCEAN.N_neg, 5), "You could get %menu for %price, how about it?"},

            // state 6
            { GetDictionaryString(TextOCEAN.O_pos, 6), "Of course, I added your burger. Could I also get something for you to drink?" },
            { GetDictionaryString(TextOCEAN.O_neg, 6), "Ok. Do you want something to drink as well?"},
            { GetDictionaryString(TextOCEAN.C_pos, 6), "I added your burger to the order, do you want something to drink with it?"},
            { GetDictionaryString(TextOCEAN.C_neg, 6), "Oh, ok... I added your burger. Do you want anything else? I mean do you want a drink?"},
            { GetDictionaryString(TextOCEAN.E_pos, 6), "I added you burger, my friend. If you want, I could also get you some drink? How about that?"},
            { GetDictionaryString(TextOCEAN.E_neg, 6), "Ok. Want some drink?" },
            { GetDictionaryString(TextOCEAN.A_pos, 6), "Yes, I added you burger. Could I get you something to drink as well?" },
            { GetDictionaryString(TextOCEAN.A_neg, 6), "Burger is added. Want some drink?"},
            { GetDictionaryString(TextOCEAN.N_pos, 6), "Hmmm, you burger is added... Do you... Do you want a drink with it?"},
            { GetDictionaryString(TextOCEAN.N_neg, 6), "Sure, could I get you a drink as well?"},

            // state 7
            { GetDictionaryString(TextOCEAN.O_pos, 7), "Unfortunately we do not have %product, however I could get you something else." },
            { GetDictionaryString(TextOCEAN.O_neg, 7), "Don't have %product, want something else?"},
            { GetDictionaryString(TextOCEAN.C_pos, 7), "We do not sell %product, but I could get you something else."},
            { GetDictionaryString(TextOCEAN.C_neg, 7), "Ok... But we do not sell that, sorry. No %product. Want something else?"},
            { GetDictionaryString(TextOCEAN.E_pos, 7), "Oh, I'm so sorry my friend, but we don't have %product. Can I get you something else?"},
            { GetDictionaryString(TextOCEAN.E_neg, 7), "Sorry, no %product. Want some other thing?" },
            { GetDictionaryString(TextOCEAN.A_pos, 7), "We do not have %product, however I could get you something else." },
            { GetDictionaryString(TextOCEAN.A_neg, 7), "Don't have %product."},
            { GetDictionaryString(TextOCEAN.N_pos, 7), "Oh, terrible... We don't have %product, but... I could get you something else?"},
            { GetDictionaryString(TextOCEAN.N_neg, 7), "We don't have %product, but I could get you something else."},

            // state 8
            { GetDictionaryString(TextOCEAN.O_pos, 8), "Of course. Would you like the large selection for your menu? It would be %price in total." },
            { GetDictionaryString(TextOCEAN.O_neg, 8), "Ok, do you want the large selection for %price?"},
            { GetDictionaryString(TextOCEAN.C_pos, 8), "Sure, it is also possible to get large selection for %price, would you like it?"},
            { GetDictionaryString(TextOCEAN.C_neg, 8), "Ok. Oh, also, you can get the large selection for %price... Do you want it?"},
            { GetDictionaryString(TextOCEAN.E_pos, 8), "Sure my friend, if you want you can also get the large selection. It is only %price, how about it?"},
            { GetDictionaryString(TextOCEAN.E_neg, 8), "Want the large selection for %price?" },
            { GetDictionaryString(TextOCEAN.A_pos, 8), "Sure. If you would like, I could get you the large selection for %price, is that ok?" },
            { GetDictionaryString(TextOCEAN.A_neg, 8), "Yup. Want to make it large for %price?"},
            { GetDictionaryString(TextOCEAN.N_pos, 8), "Well, ok. Oh... We can also make it the large selection for %price. How is that? Do you want the large selection? "},
            { GetDictionaryString(TextOCEAN.N_neg, 8), "Ok, if you want the large selection it becomes %price in total. How about it?"},

            // state 9
            { GetDictionaryString(TextOCEAN.O_pos, 9), "Would you like to get any other product?" },
            { GetDictionaryString(TextOCEAN.O_neg, 9), "Anything else?"},
            { GetDictionaryString(TextOCEAN.C_pos, 9), "Would you like to add any other product to your order?"},
            { GetDictionaryString(TextOCEAN.C_neg, 9), "Do you want anything else? Any other product you want?"},
            { GetDictionaryString(TextOCEAN.E_pos, 9), "Can I help you with anything else my friend?"},
            { GetDictionaryString(TextOCEAN.E_neg, 9), "Want anything else?" },
            { GetDictionaryString(TextOCEAN.A_pos, 9), "Could I get you anything else to eat?" },
            { GetDictionaryString(TextOCEAN.A_neg, 9), "Is that enough, or do you want anything else?"},
            { GetDictionaryString(TextOCEAN.N_pos, 9), "Umm, can I? Can I help you? With some other thing to eat?"},
            { GetDictionaryString(TextOCEAN.N_neg, 9), "Would you like to order anything else?"},
            
            // state 10
            { GetDictionaryString(TextOCEAN.O_pos, 10), "Of course, I added your %product to your order. Would you like to get anything else?" },
            { GetDictionaryString(TextOCEAN.O_neg, 10), "Sure, %product is added, anything else?"},
            { GetDictionaryString(TextOCEAN.C_pos, 10), "Ok, I added your %product, would you like to add any other product?"},
            { GetDictionaryString(TextOCEAN.C_neg, 10), "%product, right? Ok, I added it. Do you want anything else? Any other product?"},
            { GetDictionaryString(TextOCEAN.E_pos, 10), "I added your %product, my friend, is there anything else you want?"},
            { GetDictionaryString(TextOCEAN.E_neg, 10), "%product, ok. Anything else?" },
            { GetDictionaryString(TextOCEAN.A_pos, 10), "Your %product is added, could I get you anything else?" },
            { GetDictionaryString(TextOCEAN.A_neg, 10), "%product, huh? Ok, want anything else?"},
            { GetDictionaryString(TextOCEAN.N_pos, 10), "%product... Ok, ok I added it. Can I get you something else? Some other thing to eat or drink?"},
            { GetDictionaryString(TextOCEAN.N_neg, 10), "I added your %product, would you like to order anything else?"},

            // state 11
            { GetDictionaryString(TextOCEAN.O_pos, 11), "Of course, it would be %price in total, how would you like to pay your order? Cash or credit card?" },
            { GetDictionaryString(TextOCEAN.O_neg, 11), "Ok, total is %price, how will you pay it? Cash or credit?"},
            { GetDictionaryString(TextOCEAN.C_pos, 11), "Sure, it would be %price in total. Would you like to pay it with cash or credit card?"},
            { GetDictionaryString(TextOCEAN.C_neg, 11), "Ok, total price is... One second... It is %price. How will you pay it? Cash or credit?"},
            { GetDictionaryString(TextOCEAN.E_pos, 11), "Sure my friend, the total price is %price. Would you like to pay it with cash or credit card?"},
            { GetDictionaryString(TextOCEAN.E_neg, 11), "Total is %price. Would you like to pay it with cash or credit card?"},
            { GetDictionaryString(TextOCEAN.A_pos, 11), "It would be %price in total. How would you like to pay the amount? You may use cash or your credit card." },
            { GetDictionaryString(TextOCEAN.A_neg, 11), "The total is %price, if that's all. Want to pay it with cash? Or Credit card?"},
            { GetDictionaryString(TextOCEAN.N_pos, 11), "Oh, ok... The total is... It would be %price, how will you pay it? Cash or credit card?"},
            { GetDictionaryString(TextOCEAN.N_neg, 11), "Ok, it would be %price in total, how will you pay it? Cash or credit card?"},

            // state 12
            { GetDictionaryString(TextOCEAN.O_pos, 12), "The order will be ready soon. Thank you very much, please visit again." },
            { GetDictionaryString(TextOCEAN.O_neg, 12), "Order is preparing. See you later."},
            { GetDictionaryString(TextOCEAN.C_pos, 12), "Your order is going to be ready in a minute. I would like to thank you, please visit again."},
            { GetDictionaryString(TextOCEAN.C_neg, 12), "Oh, ok. The order will be ready, soon... Thank you for coming... I... I hope to see you again."},
            { GetDictionaryString(TextOCEAN.E_pos, 12), "Ok my friend, the order will be ready in a minute. Please visit us again."},
            { GetDictionaryString(TextOCEAN.E_neg, 12), "Order will be ready, please come again."},
            { GetDictionaryString(TextOCEAN.A_pos, 12), "The order will be ready in a minute. Thank you very much for coming, please visit us again. Have a nice day."},
            { GetDictionaryString(TextOCEAN.A_neg, 12), "Order is preparing. Do visit us again."},
            { GetDictionaryString(TextOCEAN.N_pos, 12), "Oh, ok... You order, it will be ready in two or three minutes. By the way, thank you for coming... Visit us again, please." },
            { GetDictionaryString(TextOCEAN.N_neg, 12), "Your order will be ready in a minute, please visit us again." },
            
            // state 13
            { GetDictionaryString(TextOCEAN.O_pos, 13), "Of course. Would you like any side products with your meal? We have fries and salad."},
            { GetDictionaryString(TextOCEAN.O_neg, 13), "Do you want any side meals?"},
            { GetDictionaryString(TextOCEAN.C_pos, 13), "Sure. Would you like any side meals in your order? I could get you a salad or fries."},
            { GetDictionaryString(TextOCEAN.C_neg, 13), "Oh, ok. By the way, do you like any side meals? There is fries... And also salad."},
            { GetDictionaryString(TextOCEAN.E_pos, 13), "Sure my friend, by the way do you want any side meals? I can give you salad or fries."},
            { GetDictionaryString(TextOCEAN.E_neg, 13), "Ok. Want any side meals?"},
            { GetDictionaryString(TextOCEAN.A_pos, 13), "Sure. If you would like any side meals you may order fries or salad."},
            { GetDictionaryString(TextOCEAN.A_neg, 13), "Do you also want any side meals?"},
            { GetDictionaryString(TextOCEAN.N_pos, 13), "Umm, sure, ok. Do you want any side meals? We have, umm, salad and fries... As side meal."},
            { GetDictionaryString(TextOCEAN.N_neg, 13), "Ok. Would you like any side meals?"}, 
            
            /*
            // state 13
            { GetDictionaryString(TextOCEAN.O_pos, 13), ""},
            { GetDictionaryString(TextOCEAN.O_neg, 13), ""},
            { GetDictionaryString(TextOCEAN.C_pos, 13), ""},
            { GetDictionaryString(TextOCEAN.C_neg, 13), ""},
            { GetDictionaryString(TextOCEAN.E_pos, 13), ""},
            { GetDictionaryString(TextOCEAN.E_neg, 13), ""},
            { GetDictionaryString(TextOCEAN.A_pos, 13), ""},
            { GetDictionaryString(TextOCEAN.A_neg, 13), ""},
            { GetDictionaryString(TextOCEAN.N_pos, 13), ""},
            { GetDictionaryString(TextOCEAN.N_neg, 13), ""}

            */
        };
        
    }

    public override void SaveAllToWav()
    {
        throw new System.NotImplementedException();
    }
}
