﻿Imports Microsoft.VisualBasic
Imports System

Partial Public Class VEC_DataInputWindowsFormsApplication
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary> 
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (Not components Is Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
        Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
        Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
        Me.module3 = New VEC_DataInput.[Module].VEC_DataInputModule()
        Me.module4 = New VEC_DataInput.[Module].Win.VEC_DataInputWindowsFormsModule()
        Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
        Me.securityStrategyComplex1 = New DevExpress.ExpressApp.Security.SecurityStrategyComplex()
        Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard()
        Me.objectsModule = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
        Me.conditionalAppearanceModule = New DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule()
        Me.reportsModuleV2 = New DevExpress.ExpressApp.ReportsV2.ReportsModuleV2()
        Me.reportsWindowsFormsModuleV2 = New DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2()
        Me.validationModule = New DevExpress.ExpressApp.Validation.ValidationModule()
        Me.validationWindowsFormsModule = New DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'securityStrategyComplex1
        '
        Me.securityStrategyComplex1.Authentication = Me.authenticationStandard1
        Me.securityStrategyComplex1.RoleType = GetType(VEC_DataInput.[Module].Role)
        Me.securityStrategyComplex1.SupportNavigationPermissionsForTypes = False
        Me.securityStrategyComplex1.UseOptimizedPermissionRequestProcessor = False
        Me.securityStrategyComplex1.UserType = GetType(VEC_DataInput.[Module].User)
        '
        'authenticationStandard1
        '
        Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
        '
        'reportsModuleV2
        '
        Me.reportsModuleV2.EnableInplaceReports = True
        Me.reportsModuleV2.ReportDataType = GetType(DevExpress.Persistent.BaseImpl.ReportDataV2)
        Me.reportsModuleV2.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML
        '
        'validationModule
        '
        Me.validationModule.AllowValidationDetailsAccess = True
        Me.validationModule.IgnoreWarningAndInformationRules = False
        '
        'VEC_DataInputWindowsFormsApplication
        '
        Me.ApplicationName = "VEC_DataInput"
        Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
        Me.Modules.Add(Me.module1)
        Me.Modules.Add(Me.module2)
        Me.Modules.Add(Me.objectsModule)
        Me.Modules.Add(Me.conditionalAppearanceModule)
        Me.Modules.Add(Me.reportsModuleV2)
        Me.Modules.Add(Me.validationModule)
        Me.Modules.Add(Me.module3)
        Me.Modules.Add(Me.reportsWindowsFormsModuleV2)
        Me.Modules.Add(Me.validationWindowsFormsModule)
        Me.Modules.Add(Me.module4)
        Me.Modules.Add(Me.securityModule1)
        Me.Security = Me.securityStrategyComplex1
        Me.UseOldTemplates = False
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

#End Region

    Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
    Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
	Private module3 As Global.VEC_DataInput.Module.VEC_DataInputModule
    Private module4 As Global.VEC_DataInput.Module.Win.VEC_DataInputWindowsFormsModule
    Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
    Private securityStrategyComplex1 As DevExpress.ExpressApp.Security.SecurityStrategyComplex
    Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
    Private objectsModule As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
    Private conditionalAppearanceModule As DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule
    Private reportsModuleV2 As DevExpress.ExpressApp.ReportsV2.ReportsModuleV2
    Private reportsWindowsFormsModuleV2 As DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2
    Private validationModule As DevExpress.ExpressApp.Validation.ValidationModule
    Private validationWindowsFormsModule As DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule
End Class
