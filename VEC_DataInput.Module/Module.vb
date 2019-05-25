Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Linq
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.Model.DomainLogics
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports System.Data.Entity
Imports DevExpress.ExpressApp.Xpo

' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
Public NotInheritable Class VEC_DataInputModule
    Inherits ModuleBase
	Shared Sub New()
		DevExpress.Data.Linq.CriteriaToEFExpressionConverter.SqlFunctionsType = GetType(System.Data.Entity.SqlServer.SqlFunctions)
		DevExpress.Data.Linq.CriteriaToEFExpressionConverter.EntityFunctionsType = GetType(System.Data.Entity.DbFunctions)
		DevExpress.ExpressApp.SystemModule.ResetViewSettingsController.DefaultAllowRecreateView = False
		' Uncomment this code to delete and recreate the database each time the data model has changed.
		' Do not use this code in a production environment to avoid data loss.
		' #If DEBUG
		'     Database.SetInitializer(New DropCreateDatabaseIfModelChanges(Of VEC_DataInputDbContext)())
		' #End If 
	End Sub
    Public Sub New()
        InitializeComponent()
		BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction
    End Sub

    Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
        Dim updater As ModuleUpdater = New Updater(objectSpace, versionFromDB)
        Return New ModuleUpdater() {updater}
    End Function

    Public Overrides Sub Setup(application As XafApplication)
        MyBase.Setup(application)
        ' Manage various aspects of the application UI and behavior at the module level.
    End Sub
	Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo)
		MyBase.CustomizeTypesInfo(typesInfo)
		CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo)
	End Sub
End Class
