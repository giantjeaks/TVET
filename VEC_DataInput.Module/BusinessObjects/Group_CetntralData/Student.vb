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
<XafDisplayName("ข้อมูลนักเรียนนักศึกษา"), NavigationItem("Group_Center")>
<DefaultClassOptions()> _
Public Class Student ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        CreateDate = Now
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

    Private _VecCenterName As String
    <Browsable(False)>
    Public Property VecCenterName() As String
        Get
            Return _VecCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("VecCenterName", _VecCenterName, value)
        End Set
    End Property

    Private _GeoName As String
    <Browsable(False)>
    Public Property GeoName() As String
        Get
            Return _GeoName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("GeoName", _GeoName, value)
        End Set
    End Property

    Private _SubjtectTypeName As String
    <XafDisplayName("ประเภทวิชา"), Size(100)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(5)>
    Public Property SubjtectTypeName() As String
        Get
            Return _SubjtectTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SubjtectTypeName", _SubjtectTypeName, value)
        End Set
    End Property

    Private _MajorName As String
    <XafDisplayName("สาขาวิชา"), Size(200)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(6)>
    Public Property MajorName() As String
        Get
            Return _MajorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MajorName", _MajorName, value)
        End Set
    End Property

    Private _MinorName As String
    <XafDisplayName("สาขางาน"), Size(200)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(7)>
    Public Property MinorName() As String
        Get
            Return _MinorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MinorName", _MinorName, value)
        End Set
    End Property
    Private _CourseType As String
    <XafDisplayName("รูปแบบการศึกษา"), Size(200)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(8)>
    Public Property CourseType() As String
        Get
            Return _CourseType
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CourseType", _CourseType, value)
        End Set
    End Property




    Private _PVCH1 As Integer
    <XafDisplayName("ปวช.1"), Size(4)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(9)>
    Public Property PVCH1() As Integer
        Get
            Return _PVCH1
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PVCH1", _PVCH1, value)
        End Set
    End Property


    Private _PVCH2 As Integer
    <XafDisplayName("ปวช.2"), Size(4)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(10)>
    Public Property PVCH2() As Integer
        Get
            Return _PVCH2
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PVCH2", _PVCH2, value)
        End Set
    End Property


    Private _PVCH3 As Integer
    <XafDisplayName("ปวช.3"), Size(4)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(11)>
    Public Property PVCH3() As Integer
        Get
            Return _PVCH3
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PVCH3", _PVCH3, value)
        End Set
    End Property


    Private _PVS1 As Integer
    <XafDisplayName("ปวส.1"), Size(4)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(12)>
    Public Property PVS1() As Integer
        Get
            Return _PVS1
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PVS1", _PVS1, value)
        End Set
    End Property


    Private _PVS2 As Integer
    <XafDisplayName("ปวส.2"), Size(4)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(13)>
    Public Property PVS2() As Integer
        Get
            Return _PVS2
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PVS2", _PVS2, value)
        End Set
    End Property


    Private _TLB1 As Integer
    <XafDisplayName("ทล.บ.1"), Size(4)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(14)>
    Public Property TLB1() As Integer
        Get
            Return _TLB1
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("TLB1", _TLB1, value)
        End Set
    End Property


    Private _TLB2 As Integer
    <XafDisplayName("ทล.บ.2"), Size(4)>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(15)>
    Public Property TLB2() As Integer
        Get
            Return _TLB2
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("TLB2", _TLB2, value)
        End Set
    End Property

    Private _CreateDate As DateTime
    <Browsable(False)>
    Public Property CreateDate As DateTime
        Get
            Return _CreateDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("CreateDate", _CreateDate, value)
        End Set
    End Property

    <XafDisplayName("จำนวนรวม")>
    <ImmediatePostData(), VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(False)>
    <Index(16)>
    Public ReadOnly Property strStudentTotal() As Integer
        Get

            Return PVCH1 + PVCH2 + PVCH3 + PVS1 + PVS2 + TLB1 + TLB2

        End Get
    End Property


End Class
