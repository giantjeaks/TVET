Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Layout
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Model.NodeGenerators

' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
Partial Public Class ctlShowDataByUser
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        ' Target required Views (via the TargetXXX properties) and create their Actions.
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        ' Perform various tasks depending on the target View.

    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        '   Access And customize the target View control.
        Try
            'สร้าง Obj ของ User ที่ Login เข้ามาเพื่อตรวจสอบว่าอยู่ในระดับใด
            Dim objUser As User = ObjectSpace.FindObject(Of User)(CriteriaOperator.Parse("Oid=?", CType(SecuritySystem.CurrentUser, User).Oid))
            If objUser IsNot Nothing Then

                If CType(SecuritySystem.CurrentUser, User).UserName.Contains("admin") = False Then
                    '  กรณี Login ไม่ใช่ admin

                    'Select case ตามคลาสที่มี View (จะกำหนดอะไรในวิวนั้นบ้าง)
                    Select Case View.Id

                        Case "EEC_Indus4TeacherReg_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                '   แปลว่า User นี้อยู่ในระดับผู้ใช้ทั่วไปตามวิทยาลัย
                                CType(View, ListView).CollectionSource.Criteria("Int_EEC_Indus4TeacherReg_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                '  สิทธิ์ตามศูนย์สถาบัน
                                CType(View, ListView).CollectionSource.Criteria("Int_EEC_Indus4TeacherReg_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                        Case "Student_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                '   แปลว่า User นี้อยู่ในระดับผู้ใช้ทั่วไปตามวิทยาลัย
                                CType(View, ListView).CollectionSource.Criteria("Int_Student_ListView") = CriteriaOperator.Parse("InstitutionName=?", objUser.InstitutionID.InstitutionName)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                '  สิทธิ์ตามศูนย์สถาบัน
                                CType(View, ListView).CollectionSource.Criteria("Int_Studentg_ListView") = CriteriaOperator.Parse("VecCenterName=?", objUser.vecCenterID.centerName)
                            End If


                        Case "PromoteWork_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_PromoteWork_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_PromoteWork_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                        Case "ScholarshipProject_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("IntA_ScholarshipProject_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("IntA_ScholarshipProjectl_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                        Case "PromoteInnvtEnt_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_PromoteInnvtEnt_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_PromoteInnvtEnt_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If
                        Case "InnovationStory_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_InnovationStory_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_InnovationStory_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                        Case "ResourceUses_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_ResourceUses_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_ResourceUses_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                        Case "Evaluate_Institution_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Evaluate_Institution_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Evaluate_Institution_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                        Case "ExtendSkill_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_ExtendSkill_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Center_ExtendSkill_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                        Case "MajorToOpen_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_MajorToOpen_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                                Exit Sub
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_MajorToOpen_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If
                        Case "HaveTheJobExploreResult_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_HaveTheJobExploreResult_ListView") = CriteriaOperator.Parse("InstitutionName=?", objUser.InstitutionID.InstitutionName)
                                Exit Sub
                            End If
                        Case "PorfomanceTesting_ListView"  'Class แบบที่มีแต่สถานศึกษา
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_PorfomanceTesting_ListView") = CriteriaOperator.Parse("Institution=?", objUser.InstitutionID)
                            End If

                        Case "Innovation_ListView"  'Class แบบที่มีแต่สถานศึกษา
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Innovation_ListView") = CriteriaOperator.Parse("Institution=?", objUser.InstitutionID)
                            End If

                        Case "Personnel_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Personnel_ListView") = CriteriaOperator.Parse("InstitutionName=?", objUser.InstitutionID.InstitutionName)
                            End If

                        Case "Student_ShortCourse_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Student_ShortCourse_ListView") = CriteriaOperator.Parse("InstitutionName=?", objUser.InstitutionID.InstitutionName)
                            End If
                        Case "GetSupportResource_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_GetSupportResource_ListView") = CriteriaOperator.Parse("Institution=?", objUser.InstitutionID)
                            End If
                        Case "Personnel_Private_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Personnel_Private_ListView") = CriteriaOperator.Parse("InstitutionName=?", objUser.InstitutionID.InstitutionName)
                            End If
                        Case "Student_Graduate_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Student_Graduate_ListView") = CriteriaOperator.Parse("InstitutionName=?", objUser.InstitutionID.InstitutionName)
                            End If
                        Case "CourseOpen_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_CourseOpen_ListView") = CriteriaOperator.Parse("InstitutionName=?", objUser.InstitutionID.InstitutionName)
                            End If

                        Case "Coordinate_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Coordinate_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_Coordinate_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                        Case "CreateAwareness_ListView"
                            If objUser.InstitutionID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_CreateAwareness_ListView") = CriteriaOperator.Parse("InstitutionID=?", objUser.InstitutionID)
                            End If
                            If objUser.InstitutionID Is Nothing And objUser.vecCenterID IsNot Nothing Then
                                CType(View, ListView).CollectionSource.Criteria("Int_CreateAwareness_ListView") = CriteriaOperator.Parse("centerId=?", objUser.vecCenterID)
                            End If

                    End Select
                End If

            End If




        Catch ex As Exception

        End Try
    End Sub
    Protected Overrides Sub OnDeactivated()
        ' Unsubscribe from previously subscribed events and release other references and resources.
        MyBase.OnDeactivated()
    End Sub
End Class
