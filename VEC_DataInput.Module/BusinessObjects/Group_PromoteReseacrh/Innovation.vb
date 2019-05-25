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
Imports System.Globalization

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()>
<XafDisplayName("ผลงานสิ่งประดิษฐ์"), NavigationItem("Group_Research")>
Public Class Innovation ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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


            Dim yearNow As String = DateTime.Now.ToString("yyyy", New CultureInfo("th-TH"))
            Dim edd As mas_Year = Session.FindObject(Of mas_Year)(CriteriaOperator.Parse("YearName=?", yearNow))
            If edd IsNot Nothing Then
                EducateYear = edd
            Else
                edd = New mas_Year(Session)
                edd.YearName = yearNow
                EducateYear = edd
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
    <Browsable(False)>
    <ImmediatePostData()>
    <Persistent("EducationYear")>
    Public ReadOnly Property EducationYear As String
        Get
            Return _EducationYear
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
            If EducateYear IsNot Nothing Then
                _EducationYear = EducateYear.YearName
            End If
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
    <XafDisplayName("สถานศึกษา")>
    <DataSourceProperty("provinceID.Institutions"), ImmediatePostData(), Index(4)>
    <Appearance("EnableInstitution_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property Institution() As Institution
        Get
            Return _Institution
        End Get
        Set(ByVal value As Institution)
            SetPropertyValue("Institution", _Institution, value)
        End Set
    End Property


    Private _InnovationDagreeLevel As InnovationDagreeLevel
    <XafDisplayName("ระดับการศึกษา"), Index(5)>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property InnovationDagreeLevel() As InnovationDagreeLevel
        Get
            Return _InnovationDagreeLevel
        End Get
        Set(ByVal value As InnovationDagreeLevel)
            SetPropertyValue("InnovationDagreeLevel", _InnovationDagreeLevel, value)
        End Set
    End Property

    Private _AwardLevel As EnumInfo.AwardLevel
    <XafDisplayName("ระดับรางวัล")>
    <RuleRequiredField(DefaultContexts.Save), Index(5)>
    Public Property AwardLevel() As EnumInfo.AwardLevel
        Get
            Return _AwardLevel
        End Get
        Set(ByVal value As EnumInfo.AwardLevel)
            SetPropertyValue("AwardLevel", _AwardLevel, value)
        End Set
    End Property

    Private _InnovationType As InnovationType
    <XafDisplayName("ประเภทผลงาน"), ToolTip("ประเภทผลงาน")>
    <RuleRequiredField(DefaultContexts.Save), Index(6)>
    Public Property InnovationType() As InnovationType
        Get
            Return _InnovationType
        End Get
        Set(ByVal value As InnovationType)
            SetPropertyValue("InnovationType", _InnovationType, value)
        End Set
    End Property

    Private _InnovationName As String
    <XafDisplayName("ชื่อผลงาน/สิงประดิษฐ์"), Size(200)>
    <RuleRequiredField(DefaultContexts.Save), Index(7)>
    Public Property InnovationName() As String
        Get
            Return _InnovationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InnovationName", _InnovationName, value)
        End Set
    End Property

    Private _MoneySupplied As String
    <XafDisplayName("แหล่งที่มาของเงินทุน"), ToolTip("แหล่งที่มาของเงินทุน"), Size(1000)>
    <RuleRequiredField(DefaultContexts.Save), Index(8)>
    Public Property MoneySupplied() As String
        Get
            Return _MoneySupplied
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MoneySupplied", _MoneySupplied, value)
        End Set
    End Property
End Class
