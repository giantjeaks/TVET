Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

Imports System.Runtime.Serialization
'Imports CustomLogonParametersExample.Module.BusinessObjects
Imports DevExpress.ExpressApp.Utils

<DomainComponent, Serializable, System.ComponentModel.DisplayName("เข้าสู่ระบบ")>
Public Class CustomLogonParameters ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Implements ISerializable

    Private _employeeUser As String
    '<DataSourceProperty("AvailableUsers"), ImmediatePostData>
    <DC.XafDisplayName("ผู้ใช้งาน(User):")>
    <DataSourceProperty("AvailableUsers")>
    Public Property EmployeeUser() As String
        Get
            Return _employeeUser
        End Get
        Set(ByVal value As String)
            If _employeeUser Is value OrElse value Is Nothing Then
                Return
            End If
            _employeeUser = value
            'Company = _employee.Company
            UserName = _employeeUser '_employee.UserName
        End Set
    End Property

    Private _ComapnyNameAPI As String
    <DC.XafDisplayName("ผู้ใช้งาน(User):")>
    <DataSourceProperty("AvailableUsers")>
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property ComapnyNameAPI() As String
        Get
            Return _ComapnyNameAPI
        End Get
        Set(ByVal value As String)
            If _ComapnyNameAPI Is value OrElse value Is Nothing Then
                Return
            End If
            _ComapnyNameAPI = value
        End Set
    End Property

    ' ISerializable 
    Public Sub New()
    End Sub
    Public Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        If info.MemberCount > 0 Then
            UserName = info.GetString("UserName")
            _password = info.GetString("Password")
        End If
    End Sub
    <System.Security.SecurityCritical>
    Public Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) _
    Implements ISerializable.GetObjectData
        info.AddValue("UserName", UserName)
        info.AddValue("Password", _password)
    End Sub
    Private Sub RefreshAvailableUsers()
        'If availableUsers Is Nothing Then
        '    Return
        'End If
        'If Company Is Nothing Then
        '    availableUsers.Criteria = Nothing
        'Else
        '    availableUsers.Criteria = New BinaryOperator("Company", Company)
        'End If
        'If employee IsNot Nothing Then
        '    If (availableUsers.IndexOf(employee) = -1) OrElse (employee.Company <> Company) Then
        '        Employee = Nothing
        '    End If
        'End If
    End Sub
    <Browsable(False)>
    Public Property UserName() As String
    Private _password As String
    <DC.XafDisplayName("รหัสผ่าน(Password):")>
    <PasswordPropertyText(True)>
    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            If _password = value Then
                Return
            End If
            _password = value
        End Set
    End Property

    ''<DataSourceProperty("AvailableUsers"), ImmediatePostData>    
    'Private _CheckLDAP As Boolean
    '<DC.XafDisplayName("กรณีใช้ LDAP")> _
    '<NonPersistent> _
    'Public Property CheckLDAP() As Boolean
    '    Get
    '        Return _CheckLDAP
    '    End Get
    '    Set(ByVal value As Boolean)
    '        'If _CheckLDAP = False Then
    '        '    Return
    '        'End If
    '        _CheckLDAP = value
    '        'Company = _employee.Company
    '        ' UserName = _employeeUser '_employee.UserName
    '    End Set
    'End Property
    Private _objectSpace As IObjectSpace
    'Private _availableCompanies As XPCollection(Of Company)
    'Private _availableUsers As XPCollection(Of Employee)
    <Browsable(False)>
    Public Property ObjectSpace() As IObjectSpace
        Get
            Return _objectSpace
        End Get
        Set(ByVal value As IObjectSpace)
            _objectSpace = value
        End Set
    End Property
End Class
