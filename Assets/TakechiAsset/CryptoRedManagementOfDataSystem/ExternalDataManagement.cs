using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ExternalData
{
    public class ExternalDataManagement : Read
    {
        protected override void DataSave<DataClass>(DataClass dataClass, string saveFilePath)
        {
            base.DataSave(dataClass , saveFilePath);
        }

        protected override DataClass LoadData<DataClass>(DataClass dataClass , string saveFilePath)
        {
            return base.LoadData(dataClass , saveFilePath);
        }
    }
}
