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
<XafDisplayName("การใช้ทรัพยากร")>
<RuleCriteria("QuantUseMustNotZero", DefaultContexts.Save, "QuantityUses > 0 ", "กรุณากรอกจำนวนที่ใช้ให้ถูกต้อง")>
Public Class ResourceUses ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Me.LastUpdated = Date.Now
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
    'Private _EducationYear As String
    '<XafDisplayName("ปีการศึกษา"), VisibleInListView(True), VisibleInDetailView(True), Size(4), RuleRequiredField(DefaultContexts.Save), Index(0)>
    'Public Property EducationYear As String
    '    Get
    '        Return _EducationYear
    '    End Get
    '    Set(value As String)
    '        SetPropertyValue("EducationYear", _EducationYear, value)
    '    End Set
    'End Property

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
    <DataSourceProperty("geoId.vecCenters"), ImmediatePostData(), Index(2)>
    <Appearance("EnablecenterId_Center", Enabled:=False, Context:="DetailView", Criteria:="userCenter")>
    <Appearance("EnablecenterId_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("ศูนย์ฯ กลุ่มจังหวัด"), RuleRequiredField(DefaultContexts.Save), Size(2)>
    Public Property centerId As mas_vecCenter
        Get
            Return _centerId
        End Get
        Set(value As mas_vecCenter)
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
        Set(value As mas_province)
            SetPropertyValue("provinceID", _provinceID, value)
        End Set
    End Property

    Private _InstitutionID As Institution
    <DataSourceProperty("provinceID.Institutions"), ImmediatePostData(), Index(4)>
    <Appearance("EnableInstitutionID_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    <XafDisplayName("สถานศึกษา"), RuleRequiredField(DefaultContexts.Save), Size(10)>
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
            SetPropertyValue("InstitutionID", _degreeLevelId, value)
            If degreeLevelId IsNot Nothing Then
                subjectTypeId = Nothing
                majorId = Nothing
                minorId = Nothing
            End If
        End Set
    End Property

    Private _subjectTypeId As mas_SubjectType
    <ImmediatePostData()>
    <XafDisplayName("ประเภทวิชา"), RuleRequiredField(DefaultContexts.Save), Size(2), Index(6)>
    Public Property subjectTypeId As mas_SubjectType
        Get
            Return _subjectTypeId
        End Get
        Set(value As mas_SubjectType)
            SetPropertyValue("subjectTypeId", _subjectTypeId, value)
            If subjectTypeId IsNot Nothing Then
                majorId = Nothing
                minorId = Nothing
            End If
        End Set
    End Property

    Private _major_id As mas_major
    <DataSourceCriteria("ref_mas_subjectType = '@This.subjectTypeId' AND DeegreeLevelName ='@This.degreeLevelId.degreeLevelName'")>
    <ImmediatePostData()>
    <XafDisplayName("สาขาวิชา"), VisibleInDetailView(True), VisibleInLookupListView(True), VisibleInListView(True)>
    <RuleRequiredField(DefaultContexts.Save), Index(7)>
    Public Property majorId As mas_major
        Get
            Return _major_id
        End Get
        Set(ByVal value As mas_major)
            SetPropertyValue("majorId", _major_id, value)
            minorId = Nothing

        End Set
    End Property
    Private _minor_id As mas_minor
    <DataSourceProperty("majorId.minors"), ImmediatePostData()>
    <XafDisplayName("สาขางาน"), VisibleInDetailView(True), VisibleInLookupListView(True), VisibleInListView(True), RuleRequiredField(DefaultContexts.Save), Index(8)>
    Public Property minorId As mas_minor
        Get
            Return _minor_id
        End Get
        Set(ByVal value As mas_minor)
            SetPropertyValue("majorId", _minor_id, value)

        End Set
    End Property

    Private _RessourceGroup_Oid As mas_ResourceGroup
    <XafDisplayName("ประเภททรัพยากร"), RuleRequiredField(DefaultContexts.Save), Index(9)>
    Public Property RessourceGroup_Oid As mas_ResourceGroup
        Get
            Return _RessourceGroup_Oid
        End Get
        Set(value As mas_ResourceGroup)
            SetPropertyValue("RessourceGroup_Oid", _RessourceGroup_Oid, value)
            DetailsResource = Nothing
        End Set
    End Property

    Private _DetailsResource As String
    <XafDisplayName("รายละเอียดของการใช้ทรัพยากร"), RuleRequiredField(DefaultContexts.Save), Index(10)>
    Public Property DetailsResource As String
        Get
            Return _DetailsResource
        End Get
        Set(value As String)
            SetPropertyValue("DetailsResource", _DetailsResource, value)
        End Set
    End Property

    Private _OrganizationType As EnumInfo.OrganizationType
    <XafDisplayName("หน่วยงาน/องค์กรที่สนับสนุน"), VisibleInListView(True), VisibleInDetailView(True), Index(11)>
    <RuleValueComparison("ComparisonOrganize", DefaultContexts.Save, ValueComparisonType.NotEquals, EnumInfo.OrganizationType.empty)>
    Public Property OrganizationType As EnumInfo.OrganizationType
        Get
            Return _OrganizationType
        End Get
        Set(ByVal value As EnumInfo.OrganizationType)
            SetPropertyValue("OrganizationType", _OrganizationType, value)
        End Set
    End Property

    Private _QuantityUses As Double
    <ImmediatePostData()>
    <XafDisplayName("จำนวนที่ใช้"), RuleRequiredField(DefaultContexts.Save), Index(12)>
    Public Property QuantityUses As Double
        Get
            Return _QuantityUses
        End Get
        Set(value As Double)
            SetPropertyValue("QuantityUses", _QuantityUses, value)
            If Unit IsNot Nothing Then
                If Unit.UnitName = "บาท" Then
                    _QuantityValue = _QuantityUses
                End If
            End If
        End Set
    End Property

    Private _Unit As mas_Units
    <ImmediatePostData()>
    <XafDisplayName("หน่วย"), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save), Index(13)>
    Public Property Unit As mas_Units
        Get
            Return _Unit
        End Get
        Set(ByVal value As mas_Units)
            SetPropertyValue("Unit", _Unit, value)
            If Unit IsNot Nothing Then
                If Unit.UnitName = "บาท" Then
                    _QuantityValue = _QuantityUses
                End If
            End If
        End Set
    End Property

    Private _QuantityValue As Decimal
    <XafDisplayName("มูลค่า(บาท)"), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True), RuleRequiredField(DefaultContexts.Save), Index(14)>
    Public Property QuantityValue As Decimal
        Get
            Return _QuantityValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("QuantityValue", _QuantityValue, value)

        End Set
    End Property

    Private _LastUpdated As DateTime
    <XafDisplayName("วันที่ Update ล่าสุด")>
    <VisibleInDetailView(False), VisibleInListView(False)>
    Public Property LastUpdated As DateTime
        Get
            Return _LastUpdated
        End Get
        Set(value As DateTime)
            SetPropertyValue("LastUpdated", _LastUpdated, value)
        End Set
    End Property

End Class
