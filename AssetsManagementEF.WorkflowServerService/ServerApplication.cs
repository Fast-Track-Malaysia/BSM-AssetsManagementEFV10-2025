using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using System.Data.Common;
using DevExpress.ExpressApp.EF;
using AssetsManagementEF.Module.BusinessObjects;

namespace AssetsManagementEF.WorkflowServerService {
    public class ServerApplication : XafApplication {
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
			if(args.Connection != null) {
				args.ObjectSpaceProvider = new EFObjectSpaceProvider(typeof(AssetsManagementEFDbContext), TypesInfo, null, (DbConnection)args.Connection);
			}
			else {
				args.ObjectSpaceProvider = new EFObjectSpaceProvider(typeof(AssetsManagementEFDbContext), TypesInfo, null, args.ConnectionString);
			}
        }
        protected override DevExpress.ExpressApp.Layout.LayoutManager CreateLayoutManagerCore(bool simple) {
            throw new NotImplementedException();
        }
    }
}