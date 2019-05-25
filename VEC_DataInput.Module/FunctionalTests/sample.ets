;#DropDB VEC_DataInputEasyTest

#Application VEC_DataInput.Win
#Application VEC_DataInput.Web

*FillForm
 User Name = Admin
*Action Log In

#IfDef VEC_DataInput.Win

*Action About...
 
*Action OK

#EndIf

#IfDef VEC_DataInput.Web
*Action Navigation(My Details)
#EndIf
