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
<XafDisplayName("ผลสำรวจการมีงานทำ")>
Public Class HaveTheJobExploreResult ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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




    Private _EducationYear As String
    <XafDisplayName("ปีการศึกษา"), VisibleInListView(True), VisibleInDetailView(True), Size(4), RuleRequiredField(DefaultContexts.Save), Index(0)>
    Public Property EducationYear As String
        Get
            Return _EducationYear
        End Get
        Set(value As String)
            SetPropertyValue("EducationYear", _EducationYear, value)
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
        Set(ByVal value As mas_geo)
            SetPropertyValue("geoId", _geoId, value)
        End Set
    End Property


    Private _centerId As mas_vecCenter
    <DataSourceProperty("geoId.vecCenters"), ImmediatePostData(), Index(2)>
    <Appearance("EnablecenterId_Center", Enabled:=False, Context:="DetailView", Criteria:="userCenter")>
    <Appearance("EnablecenterId_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("ศูนย์ฯ กลุ่มจังหวัด"), RuleRequiredField(DefaultContexts.Save)>
    Public Property centerId As mas_vecCenter
        Get
            Return _centerId
        End Get
        Set(ByVal value As mas_vecCenter)
            SetPropertyValue("centerId", _centerId, value)
        End Set
    End Property

    Private _provinceID As mas_province
    <DataSourceProperty("centerId.Provinces"), ImmediatePostData(), Index(3)>
    <Appearance("EnableprovinceID_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("จังหวัด"), RuleRequiredField(DefaultContexts.Save)>
    Public Property provinceID As mas_province
        Get
            Return _provinceID
        End Get
        Set(ByVal value As mas_province)
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


    Private _InstitutionType As String
    <XafDisplayName("รัฐ-เอกชน"), RuleRequiredField(DefaultContexts.Save), Size(50), Index(5)>
    Public Property InstitutionType As String
        Get
            Return _InstitutionType
        End Get
        Set(value As String)
            SetPropertyValue("InstitutionType", _InstitutionType, value)
        End Set
    End Property

    Private _InstitutionNo As String
    <Browsable(False)>
    Public Property InstitutionNo As String
        Get
            Return _InstitutionNo
        End Get
        Set(value As String)
            SetPropertyValue("InstitutionNo", _InstitutionNo, value)
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

    Private _degreeLevelName As String
    <XafDisplayName("ระดับการศึกษา"), Index(8)>
    <Size(50)>
    Public Property degreeLevelName() As String
        Get
            Return _degreeLevelName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("degreeLevelName", _degreeLevelName, value)
        End Set
    End Property


    Private _subjectTypeName As String
    <XafDisplayName("ประเภทวิชา"), Index(9)>
    <Size(100)>
    Public Property subjectTypeName() As String
        Get
            Return _subjectTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("subjectTypeName", _subjectTypeName, value)
        End Set
    End Property

    Private _MajorName As String
    <XafDisplayName("สาขาวิชา"), Index(10)>
    <Size(100)>
    Public Property MajorName() As String
        Get
            Return _MajorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MajorName", _MajorName, value)
        End Set
    End Property

    Private _MinorName As String
    <XafDisplayName("สาขางาน"), Index(11)>
    <Size(100)>
    Public Property MinorName() As String
        Get
            Return _MinorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MinorName", _MinorName, value)
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
    <Index(14)>
    Public Property VecCenterName() As String
        Get
            Return _VecCenterName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("VecCenterName", _VecCenterName, value)
        End Set
    End Property

    Private _WorkStatus As Integer
    <ImmediatePostData(), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)>
    Public Property WorkStatus() As Integer
        Get
            Return _WorkStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("WorkStatus", _WorkStatus, value)
        End Set
    End Property

    <ImmediatePostData(), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ทำงาน")>
    <Index(15)>
    Public ReadOnly Property ShowWorkStatus() As Integer
        Get
            Return WorkStatus - Work_ContinueToStudy
        End Get
    End Property

    Private _ContinueToStudy As Integer
    <ImmediatePostData(), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)>
    Public Property ContinueToStudy() As Integer
        Get
            Return _ContinueToStudy
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("ContinueToStudy", _ContinueToStudy, value)
        End Set
    End Property

    <ImmediatePostData(), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    <XafDisplayName("เรียนต่อ")>
    <Index(16)>
    Public ReadOnly Property ShowContinueToStudy() As Integer
        Get
            Return ContinueToStudy - Work_ContinueToStudy
        End Get
    End Property

    Private _Work_ContinueToStudy As Integer
    <XafDisplayName("เรียนและทำงานด้วย")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Index(17)>
    Public Property Work_ContinueToStudy() As Integer
        Get
            Return _Work_ContinueToStudy
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Work_ContinueToStudy", _Work_ContinueToStudy, value)
        End Set
    End Property

    Private _OtherStatus As Integer
    <Browsable(False)>
    Public Property OtherStatus() As Integer
        Get
            Return _OtherStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("OtherStatus", _OtherStatus, value)
        End Set
    End Property


End Class
