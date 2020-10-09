using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneRecord
{
    public string recordText;
    public int experimentNo;
    public int recordNo;
    public int caseNo;
    public bool recordIsAgent;

    public int special;

    public AudioClip recordClip;
    public OneEmotion recordEmotion;
    public string emotionText;

    public OneRecord(string recordText, int experimentNo, int recordNo, int caseNo, bool recordIsAgent)
    {
        this.recordText = recordText;
        this.recordIsAgent = recordIsAgent;
        this.experimentNo = experimentNo;
        this.recordNo = recordNo;
        this.caseNo = caseNo;
    }

    public OneRecord(string recordText, int experimentNo, int recordNo, int caseNo, bool recordIsAgent, int special)
    {
        this.recordText = recordText;
        this.recordIsAgent = recordIsAgent;
        this.experimentNo = experimentNo;
        this.recordNo = recordNo;
        this.caseNo = caseNo;
        this.special = special;
    }

    public void LoadClipAndEmotion(RecordPlayer rp)
    {
        switch(experimentNo)
        {
            /*case 1:
                recordClip = rp.clip_s[recordNo];
                emotionText = rp.text_s[recordNo].text;
                break;*/ // for silent, need to activate
            case 0:
                switch(caseNo)
                {
                    case 0:
                        recordClip = rp.clip_e0c0[recordNo];
                        emotionText = rp.text_e0c0[recordNo].text;
                        break;
                    case 1:
                        recordClip = rp.clip_e0c1[recordNo];
                        emotionText = rp.text_e0c1[recordNo].text;
                        break;
                    case 2:
                        recordClip = rp.clip_e0c2[recordNo];
                        emotionText = rp.text_e0c2[recordNo].text;
                        break;
                    case 3:
                        recordClip = rp.clip_e0c3[recordNo];
                        emotionText = rp.text_e0c3[recordNo].text;
                        break;
                    case 4:
                        recordClip = rp.clip_e0c4[recordNo];
                        emotionText = rp.text_e0c4[recordNo].text;
                        break;
                    case 5:
                        recordClip = rp.clip_e0c5[recordNo];
                        emotionText = rp.text_e0c5[recordNo].text;
                        break;
                    case 6:
                        recordClip = rp.clip_e0c6[recordNo];
                        emotionText = rp.text_e0c6[recordNo].text;
                        break;
                    case 7:
                        recordClip = rp.clip_e0c7[recordNo];
                        emotionText = rp.text_e0c7[recordNo].text;
                        break;
                    case 8:
                        recordClip = rp.clip_e0c8[recordNo];
                        emotionText = rp.text_e0c8[recordNo].text;
                        break;
                    case 9:
                        recordClip = rp.clip_e0c9[recordNo];
                        emotionText = rp.text_e0c9[recordNo].text;
                        break;
                }
                break;
            case 1: // originally 1, switched to 11 for silent
                switch (caseNo)
                {
                    case 0:
                        recordClip = rp.clip_e1c0[recordNo];
                        emotionText = rp.text_e1c0[recordNo].text;
                        break;
                    case 1:
                        recordClip = rp.clip_e1c1[recordNo];
                        emotionText = rp.text_e1c1[recordNo].text;
                        break;
                    case 2:
                        recordClip = rp.clip_e1c2[recordNo];
                        emotionText = rp.text_e1c2[recordNo].text;
                        break;
                    case 3:
                        recordClip = rp.clip_e1c3[recordNo];
                        emotionText = rp.text_e1c3[recordNo].text;
                        break;
                    case 4:
                        recordClip = rp.clip_e1c4[recordNo];
                        emotionText = rp.text_e1c4[recordNo].text;
                        break;
                    case 5:
                        recordClip = rp.clip_e1c5[recordNo];
                        emotionText = rp.text_e1c5[recordNo].text;
                        break;
                    case 6:
                        recordClip = rp.clip_e1c6[recordNo];
                        emotionText = rp.text_e1c6[recordNo].text;
                        break;
                    case 7:
                        recordClip = rp.clip_e1c7[recordNo];
                        emotionText = rp.text_e1c7[recordNo].text;
                        break;
                    case 8:
                        recordClip = rp.clip_e1c8[recordNo];
                        emotionText = rp.text_e1c8[recordNo].text;
                        break;
                    case 9:
                        recordClip = rp.clip_e1c9[recordNo];
                        emotionText = rp.text_e1c9[recordNo].text;
                        break;
                }
                break;
            case 2:
                switch (caseNo)
                {
                    case 0:
                        recordClip = rp.clip_e2c0[recordNo];
                        emotionText = rp.text_e2c0[recordNo].text;
                        break;
                    case 1:
                        recordClip = rp.clip_e2c1[recordNo];
                        emotionText = rp.text_e2c1[recordNo].text;
                        break;
                    case 2:
                        recordClip = rp.clip_e2c2[recordNo];
                        emotionText = rp.text_e2c2[recordNo].text;
                        break;
                    case 3:
                        recordClip = rp.clip_e2c3[recordNo];
                        emotionText = rp.text_e2c3[recordNo].text;
                        break;
                    case 4:
                        recordClip = rp.clip_e2c4[recordNo];
                        emotionText = rp.text_e2c4[recordNo].text;
                        break;
                }
                break;
            case 3:
                switch (caseNo)
                {
                    case 0:
                        recordClip = rp.clip_e3c0[recordNo];
                        emotionText = rp.text_e3c0[recordNo].text;
                        break;
                    case 1:
                        recordClip = rp.clip_e3c1[recordNo];
                        emotionText = rp.text_e3c1[recordNo].text;
                        break;
                    case 2:
                        recordClip = rp.clip_e3c2[recordNo];
                        emotionText = rp.text_e3c2[recordNo].text;
                        break;
                    case 3:
                        recordClip = rp.clip_e3c3[recordNo];
                        emotionText = rp.text_e3c3[recordNo].text;
                        break;
                    case 4:
                        recordClip = rp.clip_e3c4[recordNo];
                        emotionText = rp.text_e3c4[recordNo].text;
                        break;
                    case 5:
                        recordClip = rp.clip_e3c5[recordNo];
                        emotionText = rp.text_e3c5[recordNo].text;
                        break;
                    case 6:
                        recordClip = rp.clip_e3c6[recordNo];
                        emotionText = rp.text_e3c6[recordNo].text;
                        break;
                    case 7:
                        recordClip = rp.clip_e3c7[recordNo];
                        emotionText = rp.text_e3c7[recordNo].text;
                        break;
                    case 8:
                        recordClip = rp.clip_e3c8[recordNo];
                        emotionText = rp.text_e3c8[recordNo].text;
                        break;
                    case 9:
                        recordClip = rp.clip_e3c9[recordNo];
                        emotionText = rp.text_e3c9[recordNo].text;
                        break;
                }
                break;
            case 4:
                switch (caseNo)
                {
                    case 0:
                        recordClip = rp.clip_e4c0[recordNo];
                        emotionText = rp.text_e4c0[recordNo].text;
                        break;
                    case 1:
                        recordClip = rp.clip_e4c1[recordNo];
                        emotionText = rp.text_e4c1[recordNo].text;
                        break;
                    case 2:
                        recordClip = rp.clip_e4c2[recordNo];
                        emotionText = rp.text_e4c2[recordNo].text;
                        break;
                    case 3:
                        recordClip = rp.clip_e4c3[recordNo];
                        emotionText = rp.text_e4c3[recordNo].text;
                        break;
                    case 4:
                        recordClip = rp.clip_e4c4[recordNo];
                        emotionText = rp.text_e4c4[recordNo].text;
                        break;
                    case 5:
                        recordClip = rp.clip_e4c5[recordNo];
                        emotionText = rp.text_e4c5[recordNo].text;
                        break;
                    case 6:
                        recordClip = rp.clip_e4c6[recordNo];
                        emotionText = rp.text_e4c6[recordNo].text;
                        break;
                    case 7:
                        recordClip = rp.clip_e4c7[recordNo];
                        emotionText = rp.text_e4c7[recordNo].text;
                        break;
                    case 8:
                        recordClip = rp.clip_e4c8[recordNo];
                        emotionText = rp.text_e4c8[recordNo].text;
                        break;
                    case 9:
                        recordClip = rp.clip_e4c9[recordNo];
                        emotionText = rp.text_e4c9[recordNo].text;
                        break;
                }
                break;
        }

        // load emotion
        if (emotionText != null)
        {
            string[] words = emotionText.Split(' ');
            recordEmotion = new OneEmotion(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3]), float.Parse(words[4]), float.Parse(words[5]));
        }
    }
}
