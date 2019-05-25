
Imports DevExpress.ExpressApp.DC

Public Class EnumInfo
    Public Enum Quality
        <XafDisplayName("N/A")>
        empty = 0
        <XafDisplayName("ดีเยี่ยม")>
        excellent = 5
        <XafDisplayName("ดีมาก")>
        very_good = 4
        <XafDisplayName("ดี")>
        good = 3
        <XafDisplayName("พอใช้")>
        medium = 2
        <XafDisplayName("ปรับปรุง")>
        bad = 1

    End Enum

    Public Enum JobStatus
        <XafDisplayName("N/A")>
        empty = 0
        <XafDisplayName("สถานประกอบการ")>
        enterprise = 1
        <XafDisplayName("ราชการ/รัฐวิสาหกิจ")>
        rural = 2
        <XafDisplayName("ประกอบอาชีพส่วนตัว")>
        personal = 3
        <XafDisplayName("ศึกษาต่อ")>
        study = 4
        <XafDisplayName("ว่างงาน")>
        unemploy = 5

    End Enum


    Public Enum OrganizationType
        <XafDisplayName("N/A")>
        empty = 0
        <XafDisplayName("เอกชน")>
        _Private = 1
        <XafDisplayName("ภาครัฐ")>
        government = 2
        <XafDisplayName("รัฐวิสาหกิจ")>
        enterprise = 3
        <XafDisplayName("อื่นๆ")>
        Other = 4

    End Enum
    Public Enum AwardLevel
        <XafDisplayName("N/A")>
        empty = 0
        <XafDisplayName("จังหวัด")>
        province = 1
        <XafDisplayName("ภาค")>
        zone = 2
        <XafDisplayName("ประเทศ")>
        country = 3
        <XafDisplayName("นานาชาติ")>
        international = 4

    End Enum

    Public Enum IsNewMajor
        <XafDisplayName("N/A")>
        empty = 0
        <XafDisplayName("สาขาที่เปิดใหม่")>
        yes = 1
        <XafDisplayName("ไม่ใช่สาขาที่เปิดใหม่")>
        no = 2
    End Enum



End Class
