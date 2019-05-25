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
<XafDisplayName("สร้างการรับรู้(โครงการ/กิจกรรม)")>
<RuleCriteria("NumAwareNotZero", DefaultContexts.Save, "NumberOfTarget > 0", "กรุณากรอกจำนวนการรับรู้ให้ถูกต้อง")>
Public Class CreateAwareness ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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

    Private _EducateYear As mas_Year

    <XafDisplayName("ปีการศึกษา"), Size(4), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), Index(0)>
    Public Property EducateYear As mas_Year
        Get
            Return _EducateYear
        End Get
        Set(value As mas_Year)
            SetPropertyValue("EducateYear", _EducateYear, value)
            If EducateYear IsNot Nothing Then
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
        End Set
    End Property

    Private _Institution As String
    <Browsable(False)>
    Public Property Institution As String
        Get
            Return _Institution
        End Get
        Set(value As String)
            SetPropertyValue("Institution", _Institution, value)
        End Set
    End Property




    Private _AwarenessTarget As String
    <XafDisplayName("ชื่อโครงการ/กิจกรรม")>
    <Size(100), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property AwarenessTarget As String
        Get
            Return _AwarenessTarget
        End Get
        Set(value As String)
            SetPropertyValue("AwarenessTarget", _AwarenessTarget, value)
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property SpecificTarget As XPCollection(Of mas_TargetGroup)
        Get
            Dim qry = From c In Session.Query(Of mas_TargetGroup)
                      Where CType(c.TargetGroupName, String) = "นักเรียนนักศึกษา" Or
                          CType(c.TargetGroupName, String) = "ประชาชนทั่วไป" Or
                          CType(c.TargetGroupName, String) = "หน่วยงานอื่นๆ" Or
                          CType(c.TargetGroupName, String) = "ครูและบุคลากรสถานศึกษา"
                      Select c.Oid

            Return New XPCollection(Of mas_TargetGroup)(Session, New InOperator("Oid", qry))
        End Get
    End Property


    Private _TargetGroup As mas_TargetGroup
    <DataSourceProperty("SpecificTarget")>
    <XafDisplayName("กลุ่มเป้าหมาย")>
    <Size(100), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property TargetGroup As mas_TargetGroup
        Get
            Return _TargetGroup
        End Get
        Set(value As mas_TargetGroup)
            SetPropertyValue("TargetGroup", _TargetGroup, value)
        End Set
    End Property
    Private _NumberOfTarget As Integer
    <XafDisplayName("จำนวนการรับรู้")>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property NumberOfTarget As Integer
        Get
            Return _NumberOfTarget
        End Get
        Set(value As Integer)
            SetPropertyValue("NumberOfTarget", _NumberOfTarget, value)
        End Set
    End Property




    Private _AwarenessChannel As mas_AwarenessChannels
    <XafDisplayName("ช่องทางการรับรู้")>
    <VisibleInLookupListView(True), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property AwarenessChannel As mas_AwarenessChannels
        Get
            Return _AwarenessChannel
        End Get
        Set(value As mas_AwarenessChannels)
            SetPropertyValue("AwarenessChannel", _AwarenessChannel, value)
        End Set
    End Property
    Private _StartDate As DateTime
    <XafDisplayName("วันที่เริ่มต้น")>
    <VisibleInLookupListView(True), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property StartDate As DateTime
        Get
            Return _StartDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("StartDate", _StartDate, value)
        End Set
    End Property

    Private _EndDate As DateTime
    <XafDisplayName("วันที่สิ้นสุด")>
    <VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property EndDate As DateTime
        Get
            Return _EndDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("EndDate", _EndDate, value)
        End Set
    End Property
    Private _Details As String
    <XafDisplayName("รายละเอียด")>
    <Size(1000), VisibleInListView(True), VisibleInDetailView(True), RuleRequiredField(DefaultContexts.Save)>
    Public Property Details As String
        Get
            Return _Details
        End Get
        Set(value As String)
            SetPropertyValue("Details", _Details, value)
        End Set
    End Property


End Class
