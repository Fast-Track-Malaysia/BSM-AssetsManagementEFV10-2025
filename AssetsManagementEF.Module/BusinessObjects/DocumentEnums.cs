using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsManagementEF.Module.BusinessObjects
{
    //public enum DocumentStatus
    //{
    //    DRAFT = 0,
    //    OPEN = 1,
    //    CLOSE = 3
    //}

    //public enum MaintenanceStatus
    //{
    //    DRAFT = 0,
    //    OPEN = 1,
    //    WIP = 2,
    //    CLOSE = 3
    //}
    public enum DeviationRankEnum
    {
        NA = 0,
        Deviation_01 = 1,
        Extension_02 = 2,
        Extension_03 = 3,
        Extension_04 = 4


    }

    public enum MaintenanceFrequency
    {
        DAY = 0,
        MONTH = 1,
        CycleCount = 2
    }

    public enum DocumentStatus
    {
        Create = 0,
        DocPassed = 1,
        Approved = 2,
        Cancelled = 3,
        Rejected = 4,
        Closed = 5,
        Active = 6,
        Posted = 7


    }

}
