<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpreadSheetPM.aspx.cs" Inherits="AssetsManagementEF.Web.SpreadSheetPM" %>

<%@ Register assembly="DevExpress.Web.ASPxSpreadsheet.v17.1, Version=17.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpreadsheet" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxSpreadsheet ID="ASPxSpreadsheet1" runat="server" WorkDirectory="~/App_Data/WorkDirectory">
        </dx:ASPxSpreadsheet>
    
    </div>
    </form>
</body>
</html>
