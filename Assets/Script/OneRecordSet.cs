using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneRecordSet
{
    public List<OneRecord> recordSet;
    public int currentCount;
    public int currentRecord;
    public OneOCEAN agentOCEAN;

    public OneRecordSet()
    {
        recordSet = new List<OneRecord>();
        currentCount = 0;
        currentRecord = -1;
    }

    public OneRecord GetCurrentRecord()
    {
        if(currentRecord < currentCount-1)
        {
            currentRecord++;
            return recordSet[currentRecord];
        }
        else
        {
            return null;
        }
    }

    private bool same = false;
    private OneRecord sameAtHand;

    public OneRecord GetCurrentRecord2Times()
    {
        if (same)
        {
            same = false;
            return sameAtHand;
        }
        else
        {
            if (currentRecord < currentCount - 1)
            {
                currentRecord++;
                sameAtHand = recordSet[currentRecord];
                if(sameAtHand.recordIsAgent)
                {
                    same = true;
                    return sameAtHand;
                }
                else
                {
                    same = false;
                    return sameAtHand;
                }
                
            }
            else
            {
                return null;
            }
        }
    }

    public bool IsSame()
    {
        return same;
    }

    public void AddRecord(OneRecord record)
    {
        recordSet.Add(record);
        currentCount++;
    }
}
