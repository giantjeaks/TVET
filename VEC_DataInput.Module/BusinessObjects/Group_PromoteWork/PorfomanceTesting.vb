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
<XafDisplayName("การทดสอบสมรรถนะวิชาชีพ"), NavigationItem("Group_PromoteWork")>
<DefaultClassOptions()>
<RuleCriteria("PassNotOver", DefaultContexts.Save, "TotalPass <= TotalStudent && TotalStudent > 0 && TotalPass > 0 ", "กรุณากรอกจำนวนผู้เข้าสอบและจำนวนผู้ผ่านการสอบให้ถูกต้อง")>
Public Class PorfomanceTesting ' Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
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


    'Private _EducationYear As String
    '<XafDisplayName("ปีการศึกษา"), Size(4), Index(0)>
    '<RuleRequiredField(DefaultContexts.Save)>
    'Public Property EducationYear() As String
    '    Get
    '        Return _EducationYear
    '    End Get
    '    Set(ByVal value As String)
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
    <DataSourceProperty("provinceID.Institutions"), ImmediatePostData(), Index(4)>
    <XafDisplayName("ชื่อสถานศึกษา")>
    <RuleRequiredField(DefaultContexts.Save)>
    <Appearance("EnableInstitution_Intitution", Enabled:=False, Context:="DetailView", Criteria:="userIntitution")>
    Public Property Institution() As Institution
        Get
            Return _Institution
        End Get
        Set(ByVal value As Institution)
            SetPropertyValue("Institution", _Institution, value)
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
            SubjectType = Nothing
            major = Nothing
            minor = Nothing
        End Set
    End Property

    Private _SubjectType As mas_SubjectType
    <ImmediatePostData()>
    <XafDisplayName("ประเภทวิชา")>
    <RuleRequiredField(DefaultContexts.Save), Index(6)>
    Public Property SubjectType() As mas_SubjectType
        Get
            Return _SubjectType
        End Get
        Set(ByVal value As mas_SubjectType)
            SetPropertyValue("SubjectType", _SubjectType, value)
            major = Nothing
            minor = Nothing
        End Set
    End Property

    Private _major As mas_major
    <DataSourceCriteria("ref_mas_subjectType = '@This.SubjectType' AND DeegreeLevelName ='@This.degreeLevelId.degreeLevelName'")>
    <XafDisplayName("สาขาวิชา"), ImmediatePostData(), Index(7)>
    <RuleRequiredField(DefaultContexts.Save)>
    Public Property major() As mas_major
        Get
            Return _major
        End Get
        Set(ByVal value As mas_major)
            SetPropertyValue("major", _major, value)
            minor = Nothing
        End Set
    End Property

    Private _minor As mas_minor
    <DataSourceProperty("major.minors")>
    <XafDisplayName("สาขางาน")>
    <RuleRequiredField(DefaultContexts.Save), Index(8)>
    Public Property minor() As mas_minor
        Get
            Return _minor
        End Get
        Set(ByVal value As mas_minor)
            SetPropertyValue("minor", _minor, value)
        End Set
    End Property




    Private _TotalStudent As Integer
    <XafDisplayName("จำนวนผู้สอบ"), ImmediatePostData()>
    <RuleRequiredField(DefaultContexts.Save), Index(9)>
    Public Property TotalStudent() As Integer
        Get
            Return _TotalStudent
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("TotalStudent", _TotalStudent, value)
        End Set
    End Property

    Private _TotalPass As Integer
    <XafDisplayName("จำนวนที่ผ่าน"), ImmediatePostData()>
    <RuleRequiredField(DefaultContexts.Save), Index(10)>
    Public Property TotalPass() As Integer
        Get
            Return _TotalPass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("TotalPass", _TotalPass, value)
        End Set
    End Property




End Class
