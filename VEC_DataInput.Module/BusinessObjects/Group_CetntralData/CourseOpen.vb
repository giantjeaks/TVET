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
<XafDisplayName("ข้อมูลหลักสูตรที่สถานศึกษาเปิดรายปีการศึกษา"), NavigationItem("Group_Center")>
<DefaultClassOptions()> _
Public Class CourseOpen ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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





    Private _Year As String
    <Browsable(False)>
    Public Property Year As String
        Get
            Return _Year
        End Get
        Set(value As String)
            SetPropertyValue("Year", _Year, value)
        End Set
    End Property

    Private _ProvinceName As String
    <Browsable(False)>
    Public Property ProvinceName As String
        Get
            Return _ProvinceName
        End Get
        Set(value As String)
            SetPropertyValue("ProvinceName", _ProvinceName, value)
        End Set
    End Property

    Private _InstitutionType As String
    <Browsable(False)>
    Public Property InstitutionType As String
        Get
            Return _InstitutionType
        End Get
        Set(value As String)
            SetPropertyValue("InstitutionType", _InstitutionType, value)
        End Set
    End Property

    Private _InstitutionID As String
    <XafDisplayName("รหัสสถานศึกษา")>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True)>
    <Index(5)>
    Public Property InstitutionID As String
        Get
            Return _InstitutionID
        End Get
        Set(value As String)
            SetPropertyValue("InstitutionType", _InstitutionID, value)
        End Set
    End Property

    Private _InstitutionName As String
    <Browsable(False)>
    Public Property InstitutionName As String
        Get
            Return _InstitutionName
        End Get
        Set(value As String)
            SetPropertyValue("InstitutionName", _InstitutionName, value)
        End Set
    End Property

    Private _DeegreeLavelName As String
    <XafDisplayName("ระดับการศึกษา")>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True)>
    <Index(6)>
    Public Property DeegreeLavelName As String
        Get
            Return _DeegreeLavelName
        End Get
        Set(value As String)
            SetPropertyValue("DeegreeLavelName", _DeegreeLavelName, value)
        End Set
    End Property

    Private _CourseTypeName As String
    <XafDisplayName("ระบบ")>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True)>
    <Index(7)>
    Public Property CourseTypeName As String
        Get
            Return _CourseTypeName
        End Get
        Set(value As String)
            SetPropertyValue("CourseTypeName", _CourseTypeName, value)
        End Set
    End Property

    Private _SubjectTypeName As String
    <XafDisplayName("ประเภทวิชา")>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True)>
    <Index(8)>
    Public Property SubjectTypeName As String
        Get
            Return _SubjectTypeName
        End Get
        Set(value As String)
            SetPropertyValue("SubjectTypeName", _SubjectTypeName, value)
        End Set
    End Property

    Private _MajorName As String
    <XafDisplayName("สาขาวิชา")>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True)>
    <Index(9)>
    Public Property MajorName As String
        Get
            Return _MajorName
        End Get
        Set(value As String)
            SetPropertyValue("MajorName", _MajorName, value)
        End Set
    End Property

    Private _MinorName As String
    <XafDisplayName("สาขางาน")>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True)>
    <Index(10)>
    Public Property MinorName As String
        Get
            Return _MinorName
        End Get
        Set(value As String)
            SetPropertyValue("MinorName", _MinorName, value)
        End Set
    End Property

    Private _LeaningTypeName As String
    <XafDisplayName("รูปแบบการเรียนการสอน")>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(True)>
    <Index(11)>
    Public Property LeaningTypeName As String
        Get
            Return _LeaningTypeName
        End Get
        Set(value As String)
            SetPropertyValue("LeaningTypeName", _LeaningTypeName, value)
        End Set
    End Property


End Class
