   M S B A M L   `   `   `  ���� H  DSasha Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Sasha_ProjectSasha_Project  C&clr-namespace:Sasha_Project.ViewModelsSasha_Project.ViewModels      Sasha_Project.MainWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:Sasha_Project     =
viewmodels&clr-namespace:Sasha_Project.ViewModels     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  -     � WindowStyle$
  None=�
������  � AllowsTransparency$
 True�� � Title$ Расписание��$	��700��$
��1000�� � Icon$ /Views/timetable.png�� � ShowInTaskbar$
 True�� � WindowStartupLocation$ CenterScreen=� � Topmost$ False�� � DataContext (   "Sasha_Project.ViewModels.ViewModel   � 	Resources    BorderGradientColor	 �� 
StartPoint
 �� EndPoint � Color � Offset��%z   &	        �� $		 0,0&�$	
 1,1&�.��� $ #3F888F��$ 0Q�� $ #3E5F8A��$
 0.50Q�� $ #002F55��$ 1Q�.��� .+���  �� CornerRadius$ 15��$��5q�#��[  ���� $		 0,1&�$	
 1,0&�.��� $ #003153��$ 0Q�� $ #0E294B��$
 0.75Q�._�� .+��� ���� *SelectedViewModel+� $��2�� � ColumnDefinitions �� $��* � � RowDefinitions �� $��50 ��� .+��� $��50��$ 12 0 0 0�� � Cursor$
 Hand��$
��Left=�._��� $��/Views/menu.png��$��5,2,5,2q� �� StretchDirection$ DownOnly=�*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ 	MouseDown��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *	NewPage+� $��Right=�$��0 0 5 0q�$	��100��$��1�� �� �� �� .+��� -    ��Name$ 	MiniMized��$��0��
�������$��16(�$
 Hand��$��0q�����  �� 
TargetType" ��.���  �� Property � �� �� Value �#34AEDA �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  � �� �#278BEF����  �� 
TargetType" ��.R��� $��20��$��20��$ 30��#��z��._��� $��Center=�$��Center=��� -   $ 
FullWindow��$��1��
�������$��16(�$
 Hand��$��0q����� " ��.���  � �� �#DAB334 P�  �@J�IsMouseOver$
 True��. ���  � �� �	#FFBB910A���� " ��.R��� $��20��$��20��$ 30��#��z��._��� $��Center=�$��Center=��� -   $ Closed��$��15��
�������$��16(�$
 Hand��$��0q����� " ��.���  � �� �#DA34AE P�  �@J�IsMouseOver$
 True��. ���  � �� �	#FFB94756���� " ��.R��� $��20��$��20��$ 30��#��z��._��� $��Center=�$��Center=�