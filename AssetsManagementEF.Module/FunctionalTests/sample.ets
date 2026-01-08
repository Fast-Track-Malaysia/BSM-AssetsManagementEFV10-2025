;#DropDB AssetsManagementEFEasyTest

#Application AssetsManagementEF.Win
#Application AssetsManagementEF.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef AssetsManagementEF.Win

*Action About...
 
*Action OK

#EndIf

#IfDef AssetsManagementEF.Web
*Action Navigation(My Details)
#EndIf
