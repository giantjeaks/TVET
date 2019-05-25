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
<DefaultClassOptions()>
<XafDisplayName("ข้อมูลผู้จบหลักสูตรระยะสั้น"), NavigationItem("Group_Center")>
Public Class Student_ShortCourse ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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
                Institution = objnstitution
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

    Private _Institution As Institution
    <DataSourceProperty("provinceID.Institutions"), ImmediatePostData()>
    <Appearance("EnableInstitutionID_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("สถานศึกษา"), RuleRequiredField(DefaultContexts.Save), Size(10), Index(4)>
    Public Property Institution As Institution
        Get
            Return _Institution
        End Get
        Set(value As Institution)
            SetPropertyValue("Institution", _Institution, value)
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



    Private _InstitutionID As String
    <Browsable(False)>
    Public Property InstitutionID() As String
        Get
            Return _InstitutionID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InstitutionID", _InstitutionID, value)
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

    Private _SubjectID As String
    <XafDisplayName("รหัสสาขาวิชา")>
    <Size(15)>
    <Index(5)>
    Public Property SubjectID As String
        Get
            Return _SubjectID
        End Get
        Set(value As String)
            SetPropertyValue("SubjectID", _SubjectID, value)
        End Set
    End Property
    Private _SubjectName As String
    <XafDisplayName("ชื่อวิชา")>
    <Size(100)>
    <Index(6)>
    Public Property SubjectName As String
        Get
            Return _SubjectName
        End Get
        Set(value As String)
            SetPropertyValue("SubjectName", _SubjectName, value)
        End Set
    End Property

    Private _MajorName As String
    <XafDisplayName("ชื่อสาขา")>
    <Size(200)>
    <Index(7)>
    Public Property MajorName As String
        Get
            Return _MajorName
        End Get
        Set(value As String)
            SetPropertyValue("MajorName", _MajorName, value)
        End Set
    End Property

    Private _CourceType As String
    <XafDisplayName("หลักสูตร")>
    <Size(200)>
    <Index(8)>
    Public Property CourceType As String
        Get
            Return _CourceType
        End Get
        Set(value As String)
            SetPropertyValue("CourceType", _CourceType, value)
        End Set
    End Property

    Private _Hour_Project As String
    <XafDisplayName("ชม/โครงการ")>
    <Size(200)>
    <Index(9)>
    Public Property Hour_Project As String
        Get
            Return _Hour_Project
        End Get
        Set(value As String)
            SetPropertyValue("Hour_Project", _Hour_Project, value)
        End Set
    End Property

    Private _Hour As String
    <XafDisplayName("จำนวน ชม.")>
    <Index(10)>
    Public Property Hour As String
        Get
            Return _Hour
        End Get
        Set(value As String)
            SetPropertyValue("Hour", _Hour, value)
        End Set
    End Property


    Private _StudentTotal As Integer
    <XafDisplayName("จำนวนผู้สำเร็จการศึกษา")>
    <Index(11)>
    Public Property StudentTotal As Integer
        Get
            Return _StudentTotal
        End Get
        Set(value As Integer)
            SetPropertyValue("StudentTotal", _StudentTotal, value)
        End Set
    End Property






End Class
