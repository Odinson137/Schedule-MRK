   M S B A M L   `   `   `  ����H  DSasha Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Sasha_ProjectSasha_Project  5   	   C&clr-namespace:Sasha_Project.ViewModelsSasha_Project.ViewModels  63       Sasha_Project.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Sasha_Project     5   	   =
viewmodels&clr-namespace:Sasha_Project.ViewModels     63   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   -   5          � Title$  Расписание��5
   	   $	��700��6   $
��1000��6)    � Icon$ /Views/timetable.png��66    � ShowInTaskbar$
 True��6R    � WindowStartupLocation$ CenterScreen=�6g    � Topmost$ False��6�    � DataContext 5      (   "Sasha_Project.ViewModels.ViewModel  5   
    � 	Resources 5         BorderGradientColor �� 
StartPoint �� EndPoint	 � Color
 � Offset��%�   &	        �� 5   
   $	 0,0&�6:   $	 1,1&�6K   .��5      � $	 #3F888F��6   $
 0Q�6+   � 5      $	 #3E5F8A��6   $

 0.50Q�6+   � 5      $	 #002F55��6   $
 1Q�6+   5      5      .��5      � .+�5   
   �� $��5q�5      #��[  6!   ��5      �� 5      $	 0,1&�6&   $	 1,0&�67   .��5      � $	 #003153��6#   $
 0Q�63   � 5       $	 #0E294B��6#   $

 0.75Q�63   5!      ._�5$      � .+�5%      �� ��6!   �� *SelectedViewModel+� 5'      $��2��6    � ColumnDefinitions 5(      �� 5)      $��* �6+    � RowDefinitions 5+      �� 5,      $��50 �6(   �� 5-      .+�50      �� $��50��6    �� CornerRadius$ 12 0 0 0��6(    � Cursor$
 Hand��6@   $
��Left=�6N   ._�51      �� $��/Views/menu.png��6    $��5,2,5,2q�69    �� StretchDirection$ DownOnly=�6J   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 52      +  %Microsoft.Xaml.Behaviors.EventTrigger  53         	EventName$ 	MouseDown��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 54   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 55   1   �� *	NewPage+56      58      � 5:      $��Right=�6   $��0 0 5 0q�67   $	��100��6H   $��1��6T    5;      �� 5<      �� 5=      �� 5>      5�      5�      5�      5�      5�      5�      