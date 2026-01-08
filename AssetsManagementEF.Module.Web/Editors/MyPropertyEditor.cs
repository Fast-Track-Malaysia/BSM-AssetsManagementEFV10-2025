using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using System;
using DevExpress.ExpressApp.Model;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.ExpressApp.HtmlPropertyEditor.Web;

namespace XafPosV2.Module.Web.Editors
{
    [PropertyEditor(typeof(int), true)]
    public class MyIntPropertyEditor : ASPxIntPropertyEditor
    {
        public MyIntPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            var spinEditor = control as ASPxSpinEdit;
            if (spinEditor == null) return;
            spinEditor.SpinButtons.ShowIncrementButtons = false;
            spinEditor.AllowMouseWheel = false;
        }
    }

    [PropertyEditor(typeof(double), true)]
    public class MyDoublePropertyEditor : ASPxDoublePropertyEditor
    {
        public MyDoublePropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            var spinEditor = control as ASPxSpinEdit;
            if (spinEditor == null) return;
            spinEditor.SpinButtons.ShowIncrementButtons = false;
            spinEditor.AllowMouseWheel = false;
        }
    }

    [PropertyEditor(typeof(decimal), true)]
    public class MyDecimalPropertyEditor : ASPxDecimalPropertyEditor
    {
        public MyDecimalPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            var spinEditor = control as ASPxSpinEdit;
            if (spinEditor == null) return;
            spinEditor.SpinButtons.ShowIncrementButtons = false;
            spinEditor.AllowMouseWheel = false;
        }
    }

    [PropertyEditor(typeof(float), true)]
    public class MyFloatPropertyEditor : ASPxFloatPropertyEditor
    {
        public MyFloatPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            var spinEditor = control as ASPxSpinEdit;
            if (spinEditor == null) return;
            spinEditor.SpinButtons.ShowIncrementButtons = false;
            spinEditor.AllowMouseWheel = false;
        }
    }

    //[PropertyEditor(typeof(LookupEditProperties), true)]
    //public class MyLookupPropertyEditor : ASPxLookupPropertyEditor
    //{
    //    public MyLookupPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

    //    protected override void SetupControl(WebControl control)
    //    {
    //        base.SetupControl(control);

    //    }

    //}
    [PropertyEditor(typeof(String), "HTML", false)]
    public class MyHTMLPropertyEditor : ASPxHtmlPropertyEditor
    {
        public MyHTMLPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }


        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            //control.Height = 500;
        }
    }

    //[PropertyEditor(typeof(String), "LSTR", false)]
    //public class MyStringPropertyEditor : ASPxStringPropertyEditor
    //{
    //    public MyStringPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

    //    protected override void SetupControl(WebControl control)
    //    {
    //        base.SetupControl(control);
    //        ASPxTextEdit myeditor = (ASPxTextEdit)control;
    //        if (myeditor != null)
    //        {
    //            myeditor.Height = 100;
    //        }

    //    }
    //}

}

