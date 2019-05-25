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
<NavigationItem("ฝ่ายประสานงานและสร้างการรับรู้")>
<XafDisplayName("การประสานงาน(โครงงาน/กิจกรรม)")>
<RuleCriteria("NumTargetNotZero", DefaultContexts.Save, "ValueTarget > 0", "กรุณากรอกจำนวนกลุ่มเป้าหมายให้ถูกต้อง")>
Public Class Coordinate ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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

    Private _Year As String
    <XafDisplayName("ปี")>
    <Persistent("Year")>
    <Browsable(False)>
    <Size(4)>
    Public ReadOnly Property Year As String
        Get
            Return _Year
        End Get

    End Property

    <Browsable(False)>
    Public ReadOnly Property SpecificYear As XPCollection(Of mas_Year)
        Get
            Dim qry = From c In Session.Query(Of mas_Year)
                      Where CType(c.YearName, Integer) > 2559
                      Select c.Oid

            Return New XPCollection(Of mas_Year)(Session, New InOperator("Oid", qry))
        End Get
    End Property

    Private _EducateYear As mas_Year
    <DataSourceProperty("SpecificYear")>
    <XafDisplayName("ปีการศึกษา"), Size(4), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), Index(0)>
    Public Property EducateYear As mas_Year
        Get
            Return _EducateYear
        End Get
        Set(value As mas_Year)
            SetPropertyValue("EducateYear", _EducateYear, value)
            If EducateYear IsNot Nothing Then
                _Year = EducateYear.YearName
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
            If InstitutionID IsNot Nothing Then
                _Institution = InstitutionID.InstitutionName
            End If
        End Set
    End Property




    Private _Institution As String
    <Persistent("Institution")>
    <Browsable(False)>
    Public ReadOnly Property Institution As String
        Get
            Return _Institution
        End Get

    End Property

    Private _DateTime As DateTime
    <XafDisplayName("วันที่"), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), Index(5)>
    Public Property DateTime As DateTime
        Get
            Return _DateTime
        End Get
        Set(value As DateTime)
            SetPropertyValue("DateTime", _DateTime, value)
        End Set
    End Property

    Private _ActivityName As String
    <XafDisplayName("ชื่อกิจกรรม"), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), Index(6)>
    Public Property ActivityName As String
        Get
            Return _ActivityName
        End Get
        Set(value As String)
            SetPropertyValue("ActivityName", _ActivityName, value)
        End Set
    End Property

    Private _OrganizationName As String
    <XafDisplayName("ชื่อหน่วยงาน"), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), Index(7)>
    Public Property OrganizationName As String
        Get
            Return _OrganizationName
        End Get
        Set(value As String)
            SetPropertyValue("OrganizationName", _OrganizationName, value)
        End Set
    End Property

    Private _Place As String
    <XafDisplayName("สถานที่"), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), Index(8)>
    Public Property Place As String
        Get
            Return _Place
        End Get
        Set(value As String)
            SetPropertyValue("Place", _Place, value)
        End Set
    End Property

    Private _Method As mas_Method
    <XafDisplayName("รูปแบบ"), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), Index(9)>
    Public Property Method As mas_Method
        Get
            Return _Method
        End Get
        Set(value As mas_Method)
            SetPropertyValue("Method", _Method, value)
        End Set
    End Property


    <Browsable(False)>
    Public ReadOnly Property SpecificTarget As XPCollection(Of mas_TargetGroup)
        Get
            Dim qry = From c In Session.Query(Of mas_TargetGroup)
                      Where CType(c.TargetGroupName, String) = "นักเรียนนักศึกษา" Or
                          CType(c.TargetGroupName, String) = "ประชาชนทั่วไป" Or
                          CType(c.TargetGroupName, String) = "หน่วยงานอื่นๆ" 
                      Select c.Oid

            Return New XPCollection(Of mas_TargetGroup)(Session, New InOperator("Oid", qry))
        End Get
    End Property



    Private _Target As mas_TargetGroup
    <DataSourceProperty("SpecificTarget")>
    <XafDisplayName("กลุ่มเป้าหมาย"), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), Index(10)>
    Public Property Target As mas_TargetGroup
        Get
            Return _Target
        End Get
        Set(value As mas_TargetGroup)
            SetPropertyValue("Target", _Target, value)
        End Set
    End Property


    Private _ValueTarget As Integer

    <XafDisplayName("จำนวนกลุ่มเป้าหมาย"), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save), Index(11)>
    Public Property ValueTarget As Integer
        Get
            Return _ValueTarget
        End Get
        Set(value As Integer)
            SetPropertyValue("ValueTarget", _ValueTarget, value)
        End Set
    End Property

End Class
