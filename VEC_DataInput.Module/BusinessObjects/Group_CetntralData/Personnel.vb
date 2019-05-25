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
Imports DevExpress.ExpressApp.ConditionalAppearance

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<XafDisplayName("ข้อมูลบุคลากรภาครัฐ"), NavigationItem("Group_Center")>
<DefaultClassOptions()> _
Public Class Personnel ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        Try
            Dim myUser As User
            myUser = CType(SecuritySystem.CurrentUser, User)
            If myUser.InstitutionID IsNot Nothing Then
                Dim objnstitution As Institution = Session.FindObject(Of Institution)(CriteriaOperator.Parse("Oid=?", CType(SecuritySystem.CurrentUser, User).InstitutionID.Oid))
                InstitutionID = objnstitution
                provinceID = objnstitution.ref_mas_province
                centerId = objnstitution.ref_mas_province.ref_mas_vecCenter
                geoId = objnstitution.ref_mas_province.ref_mas_vecCenter.ref_mas_geo
            ElseIf myUser.vecCenterID IsNot Nothing Then

                Dim objcenterId As mas_vecCenter = Session.FindObject(Of mas_vecCenter)(CriteriaOperator.Parse("Oid=?", CType(SecuritySystem.CurrentUser, User).vecCenterID.Oid))
                centerId = objcenterId
                geoId = objcenterId.ref_mas_geo
            End If




        Catch ex As Exception

        End Try
    End Sub



    'ใช้กับเงื่อนไขในการเช็คผู้ใช้ระดับศูนย์
    <ImmediatePostData(), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)>
    Public ReadOnly Property userCenter() As Boolean
        Get
            Dim myUser As User
            myUser = CType(SecuritySystem.CurrentUser, User)
            If myUser.vecCenterID IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    'ใช้กับเงื่อนไขในการเช็คผู้ใช้ระดับสถาบัน
    <ImmediatePostData(), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)>
    Public ReadOnly Property userIntitution() As Boolean
        Get
            Dim myUser As User
            myUser = CType(SecuritySystem.CurrentUser, User)
            If myUser.InstitutionID IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property



    Private _EducateYear As mas_Year
    <DataSourceProperty("SpecificYear")>
    <ImmediatePostData()>
    <XafDisplayName("ปีการศึกษา"), Size(4), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), Index(0)>
    Public Property EducateYear As mas_Year
        Get
            Return _EducateYear
        End Get
        Set(value As mas_Year)
            SetPropertyValue("EducateYear", _EducateYear, value)
        End Set
    End Property

    Private _geoId As mas_geo
    <ImmediatePostData()>
    <XafDisplayName("ศูนย์ฯ ภาค"), RuleRequiredField(DefaultContexts.Save), Index(1)>
    <Appearance("EnablegeoId_Center", Enabled:=False, Context:="DetailView", Criteria:="userCenter")>
    <Appearance("EnablegeoId_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    Public Property geoId As mas_geo
        Get
            Return _geoId
        End Get
        Set(value As mas_geo)
            SetPropertyValue("geoId", _geoId, value)
        End Set
    End Property

    Private _centerId As mas_vecCenter
    <DataSourceProperty("geoId.vecCenters"), ImmediatePostData()>
    <Appearance("EnablecenterId_Center", Enabled:=False, Context:="DetailView", Criteria:="userCenter")>
    <Appearance("EnablecenterId_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("ศูนย์ฯ กลุ่มจังหวัด"), RuleRequiredField(DefaultContexts.Save), Size(2), Index(2)>
    Public Property centerId As mas_vecCenter
        Get
            Return _centerId
        End Get
        Set(value As mas_vecCenter)
            SetPropertyValue("centerId", _centerId, value)
        End Set
    End Property


    Private _provinceID As mas_province
    <DataSourceProperty("centerId.Provinces"), ImmediatePostData()>
    <Appearance("EnableprovinceID_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("จังหวัด"), RuleRequiredField(DefaultContexts.Save), Index(3)>
    Public Property provinceID As mas_province
        Get
            Return _provinceID
        End Get
        Set(value As mas_province)
            SetPropertyValue("provinceID", _provinceID, value)
        End Set
    End Property

    Private _InstitutionID As Institution
    <DataSourceProperty("provinceID.Institutions"), ImmediatePostData()>
    <Appearance("EnableInstitutionID_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("สถานศึกษา"), RuleRequiredField(DefaultContexts.Save), Size(10), Index(4)>
    Public Property InstitutionID As Institution
        Get
            Return _InstitutionID
        End Get
        Set(value As Institution)
            SetPropertyValue("InstitutionID", _InstitutionID, value)
        End Set
    End Property


    Private _EducationYear As String
    <Browsable(False)>
    Public Property EducationYear() As String
        Get
            Return _EducationYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("EducationYear", _EducationYear, value)
        End Set
    End Property

    Private _ProvinceName As String
    <Browsable(False)>
    Public Property ProvinceName() As String
        Get
            Return _ProvinceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ProvinceName", _ProvinceName, value)
        End Set
    End Property

    Private _InstitutionName As String
    <Browsable(False)>
    Public Property InstitutionName() As String
        Get
            Return _InstitutionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InstitutionName", _InstitutionName, value)
        End Set
    End Property

    Private _Officer_Manager As Integer
    <XafDisplayName("ผู้บริหาร")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(5)>
    Public Property Officer_Manager() As Integer
        Get
            Return _Officer_Manager
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Officer_Manager", _Officer_Manager, value)
        End Set
    End Property

    Private _Officer_Teach As Integer
    <XafDisplayName("ครูผู้สอน")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(6)>
    Public Property Officer_Teach() As Integer
        Get
            Return _Officer_Teach
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Officer_Teach", _Officer_Teach, value)
        End Set
    End Property

    Private _Officer_KP As Integer
    <XafDisplayName("ก.พ.")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(7)>
    Public Property Officer_KP() As Integer
        Get
            Return _Officer_KP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Officer_KP", _Officer_KP, value)
        End Set
    End Property

    Private _EmployeeRegular_Teach As Integer
    <XafDisplayName("ลูกจ้างประจำทำหน้าที่สอน")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(8)>
    Public Property EmployeeRegular_Teach() As Integer
        Get
            Return _EmployeeRegular_Teach
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeRegular_Teach", _EmployeeRegular_Teach, value)
        End Set
    End Property


    Private _EmployeeRegular_General As Integer
    <XafDisplayName("ลูกจ้างประจำทั่วไป")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(9)>
    Public Property EmployeeRegular_General() As Integer
        Get
            Return _EmployeeRegular_General
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeRegular_General", _EmployeeRegular_General, value)
        End Set
    End Property


    Private _EmployeeOfficer_Teach As Integer
    <XafDisplayName("พนักงานราชการทำหน้าที่สอน")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(10)>
    Public Property EmployeeOfficer_Teach() As Integer
        Get
            Return _EmployeeOfficer_Teach
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeOfficer_Teach", _EmployeeOfficer_Teach, value)
        End Set
    End Property

    Private _EmployeeOfficer_General As Integer
    <XafDisplayName("พนักงานราชการทั่วไป")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(11)>
    Public Property EmployeeOfficer_General() As Integer
        Get
            Return _EmployeeOfficer_General
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeOfficer_General", _EmployeeOfficer_General, value)
        End Set
    End Property


    ''' <summary>
    ''' '
    ''' </summary>
    Private _EmployeeTemp_Budget As Integer
    <XafDisplayName("ลูกจ้างชั่วคราวทำหน้าที่สอนตามงบดำเนินการ")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(12)>
    Public Property EmployeeTemp_Budget() As Integer
        Get
            Return _EmployeeTemp_Budget
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeTemp_Budget", _EmployeeTemp_Budget, value)
        End Set
    End Property

    Private _EmployeeTemp_Subsidy As Integer
    <XafDisplayName("ลูกจ้างชั่วคราวทำหน้าที่สอนตามงบอุดหนุน")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(13)>
    Public Property EmployeeTemp_Subsidy() As Integer
        Get
            Return _EmployeeTemp_Subsidy
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeTemp_Subsidy", _EmployeeTemp_Subsidy, value)
        End Set
    End Property

    Private _EmployeeTemp_BenefitMonth As Integer
    <XafDisplayName("ลูกจ้างชั่วคราวทำหน้าที่สอนรายเดือน")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(14)>
    Public Property EmployeeTemp_BenefitMonth() As Integer
        Get
            Return _EmployeeTemp_BenefitMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeTemp_BenefitMonth", _EmployeeTemp_BenefitMonth, value)
        End Set
    End Property

    Private _EmployeeTemp_BenefitHour As Integer
    <XafDisplayName("ลูกจ้างชั่วคราวทำหน้าที่สอนรายชั่วโมง")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(15)>
    Public Property EmployeeTemp_BenefitHour() As Integer
        Get
            Return _EmployeeTemp_BenefitHour
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeTemp_BenefitHour", _EmployeeTemp_BenefitHour, value)
        End Set
    End Property

    Private _EmployeeGeneral_Budget As Integer
    <XafDisplayName("ลูกจ้างชั่วคราวทำหน้าที่ทั่วไปตามงบดำเนินการ")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(16)>
    Public Property EmployeeGeneral_Budget() As Integer
        Get
            Return _EmployeeGeneral_Budget
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeGeneral_Budget", _EmployeeGeneral_Budget, value)
        End Set
    End Property

    Private _EmployeeGeneral_Subsidy As Integer
    <XafDisplayName("ลูกจ้างชั่วคราวทำหน้าที่ทั่วไปตามงบเงินอุดหนุน")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(17)>
    Public Property EmployeeGeneral_Subsidy() As Integer
        Get
            Return _EmployeeGeneral_Subsidy
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeGeneral_Subsidy", _EmployeeGeneral_Subsidy, value)
        End Set
    End Property


    Private _EmployeeGeneral_BenefitMonth As Integer
    <XafDisplayName("ลูกจ้างชั่วคราวทำหน้าที่ทั่วไปรายเดือน")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(18)>
    Public Property EmployeeGeneral_BenefitMonth() As Integer
        Get
            Return _EmployeeGeneral_BenefitMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeGeneral_BenefitMonth", _EmployeeGeneral_BenefitMonth, value)
        End Set
    End Property


    Private _EmployeeGeneral_BenefitHour As Integer
    <XafDisplayName("ลูกจ้างชั่วคราวทำหน้าที่ทั่วไปรายชั่วโมง")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(19)>
    Public Property EmployeeGeneral_BenefitHour() As Integer
        Get
            Return _EmployeeGeneral_BenefitHour
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("EmployeeGeneral_BenefitHour", _EmployeeGeneral_BenefitHour, value)
        End Set
    End Property

    Private _SpecialTeacher As Integer
    <XafDisplayName("ครูภูมิปัญญาท้องถิ่น")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(20)>
    Public Property SpecialTeacher() As Integer
        Get
            Return _SpecialTeacher
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("SpecialTeacher", _SpecialTeacher, value)
        End Set
    End Property

End Class
