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
Imports DevExpress.ExpressApp.ConditionalAppearanceE
Imports System.Globalization
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Editors


'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()>
<XafDisplayName("ข้อมูลหลักสูตรที่สถานศึกษาต้องการเปิดใหม่ตรงตาม 10 อุตสาหกรรม")>
Public Class MajorToOpen ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        Try



            'Default ปีในการกรอกฟอร์ม
            Dim yearNow As String = DateTime.Now.ToString("yyyy", New CultureInfo("th-TH"))

            Dim objMasYear As mas_Year = Session.FindObject(Of mas_Year)(CriteriaOperator.Parse("YearName=?", yearNow))
            If objMasYear IsNot Nothing Then
                EducateYear = objMasYear
            Else
                objMasYear = New mas_Year(Session)
                objMasYear.YearName = yearNow
                EducateYear = objMasYear
            End If


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


    'ใช้จำกัดปีที่อยากให้แสดงใน EducateYear
    <Browsable(False)>
    Public ReadOnly Property SpecificYear As XPCollection(Of mas_Year)
        Get
            Dim qry = From c In Session.Query(Of mas_Year)
                      Where CType(c.YearName, Integer) >= 2559 And CType(c.YearName, Integer) <= 2561
                      Select c.Oid

            Return New XPCollection(Of mas_Year)(Session, New InOperator("Oid", qry))
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

    Private _degreeLevelId As mas_degreeLevel
    <ImmediatePostData()>
    <XafDisplayName("ระดับการศึกษา"), RuleRequiredField(DefaultContexts.Save), Size(2), Index(5)>
    Public Property degreeLevelId As mas_degreeLevel
        Get
            Return _degreeLevelId
        End Get
        Set(value As mas_degreeLevel)
            SetPropertyValue("degreeLevelId", _degreeLevelId, value)
            subjectTypeId = Nothing
            YearToOpen = Nothing
        End Set
    End Property

    Private _subjectTypeId As mas_SubjectType
    <XafDisplayName("ประเภทวิชา"), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save)>
    <Size(2), Index(6), ImmediatePostData()>
    Public Property subjectTypeId As mas_SubjectType
        Get
            Return _subjectTypeId
        End Get
        Set(value As mas_SubjectType)
            SetPropertyValue("subjectTypeId", _subjectTypeId, value)

            YearToOpen = Nothing
        End Set
    End Property


    Private _majorName As String
    <XafDisplayName("ชื่อสาขาที่ต้องการเปิดใหม่")>
    <RuleRequiredField(DefaultContexts.Save), Index(7)>
    Public Property majorName As String
        Get
            Return _majorName
        End Get
        Set(value As String)
            SetPropertyValue("majorName", _majorName, value)
        End Set
    End Property


    Private _minorName As String
    <XafDisplayName("ชื่อสาขางานที่ต้องการเปิดใหม่")>
    <RuleRequiredField(DefaultContexts.Save), Index(8)>
    Public Property minorName As String
        Get
            Return _minorName
        End Get
        Set(value As String)
            SetPropertyValue("minorName", _minorName, value)
        End Set
    End Property

    Private _YearToOpen As String
    <XafDisplayName("ปีที่เริ่มเปิดสาขา"), Size(4), VisibleInDetailView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), Index(9)>
    Public Property YearToOpen As String
        Get
            Return _YearToOpen
        End Get
        Set(ByVal value As String)
            SetPropertyValue("YearToOpen", _YearToOpen, value)
        End Set
    End Property





    Private _IndustryCategoryID As Mas_SpecialIndustryGroups
    <XafDisplayName("กลุ่มอุตสาหกรรมพิเศษ"), Index(11), ImmediatePostData(), RuleRequiredField(DefaultContexts.Save)>
    Public Property IndustryCategoryID() As Mas_SpecialIndustryGroups
        Get
            Return _IndustryCategoryID
        End Get
        Set(ByVal value As Mas_SpecialIndustryGroups)
            SetPropertyValue("IndustryCategoryID", _IndustryCategoryID, value)
        End Set
    End Property



    Private _IndustryTypeID As mas_specialIndustryTypes
    <XafDisplayName("ประเภทอุตสาหกรรมพิเศษ"), Index(12), ImmediatePostData(), RuleRequiredField(DefaultContexts.Save)>
    Public Property IndustryTypeID() As mas_specialIndustryTypes
        Get
            Return _IndustryTypeID
        End Get
        Set(ByVal value As mas_specialIndustryTypes)
            SetPropertyValue("IndustryTypeID", _IndustryTypeID, value)
        End Set
    End Property

    Private _LastUpdated As DateTime
    <XafDisplayName("วันที่ Update ล่าสุด")>
    <VisibleInListView(False), VisibleInDetailView(False), Index(13)>
    Public Property LastUpdated As DateTime
        Get
            Return _LastUpdated
        End Get
        Set(value As DateTime)
            SetPropertyValue("LastUpdated", _LastUpdated, value)
        End Set
    End Property


End Class
