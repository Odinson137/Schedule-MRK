   M S B A M L   `   `   `  ���� H  DSasha Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Sasha_Project.Views.SettingsViews!Sasha_Project.Views.SettingsViews  O,clr-namespace:Sasha_Project.Views.ConvertersSasha_Project.Views.Converters  _4clr-namespace:Sasha_Project.ViewModels.SettingsPages&Sasha_Project.ViewModels.SettingsPages  ;    5Sasha_Project.Views.SettingsViews.SettingsPrepodsView   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Alocal/clr-namespace:Sasha_Project.Views.SettingsViews     C
converters,clr-namespace:Sasha_Project.Views.Converters     K
viewmodels4clr-namespace:Sasha_Project.ViewModels.SettingsPages     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors    � DataContext  D   >Sasha_Project.ViewModels.SettingsPages.SettingPrepodsViewModel   � 	Resources >  8Sasha_Project.Views.Converters.ChangeToSaveTextConverter   ButtonClickText<  6Sasha_Project.Views.Converters.NewPrepodAddItemVisible  Visible �� 
TargetType   ComboBoxToggleButtonTemplate � ColumnDefinitions  MainBlueColorMiddle �� CornerRadius  MainBlueGentleColor �� Triggers P� Property P� Value �� 
TargetName  MainBlueColorBlack	 �� Value
 �� Property �� 
TargetType  	FontColor �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� 
IsReadOnly �� 
IsReadOnly � AllowsTransparency �� IsDropDownOpen J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation��%4  &	        &	      &	 
     7  7  7  7  7  7  7  7  7  7  7  7  7  '�� �    7  7  7  7  7  7      �� " d�.R��  �� �� $��20 �.+��� $��Border��$��2��8��  $ 5 5 0 0��$��
1, 1, 1, 1q�8�� �� $��ButtonBorder��$��1��$ 0 0 5 0��$��
1, 1, 1, 1q�8�� 8�� F� $��Arrow��$��1��=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=�8�� $��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ ButtonBorder��8	  
 � ��P�  �@d�	IsChecked$
 TrueY�. ��� $ ButtonBorder��8	  
 � ���� $ Arrow��8	  
 � ��P�  � {�$ False��. ��� $ Border��8	  
 � ���� $ ButtonBorder��8	 	 
 � ���� $ ButtonBorder��8	 
 
 � ���� 8	  
 � ���� $ Arrow��8	  
 � ���� " ��.��� 
 �@J�SnapsToDevicePixels$
	 True���� 
 �@�OverridesDefaultStyle$
	 True���� 
 � ��$	 Hidden=��� 
 � ��$	 Hidden=��� 
 � ��$
	 True���� 
 � ��8	   �� 
 �@�FocusVisualStyle	 Z� �� 
 � ��	 �� " ��.R�� .+�d� $��ToggleButton��$��2��$ Press=�$|�False��8��  �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=��� $��ContentSite��$��5, 3, 23, 3q�$ False��$
��Left=�$��Center=�#��z #��z #��z���� $��PART_EditableTextBox��$��3, 3, 23, 3q�# z $y�Hidden=�8�� $
��Left=�$��Center=�$��30��$��24(�$
|�True��.������ " ��.R��� $��PART_ContentHost��$|�False��.��� $��Popup��$��Bottom=�$|�False��$
 True��#��z $��Slide=�.��� $��DropDown��$
 True��#��z #��z .+��� $��DropDownBorder��8�� $��
0, 1, 0, 0q�$ 0��$��1,1,1,1q�8�� �� $��4q�$
 True��.���� $ 	Contained=� P�  �@��HasItems$ False��. ��� $ DropDownBorder��
 � ��$		 500��P�  � {�$ False��. ��� 8	  
 � ��P�  �@��
IsGrouping$
 True��. ��� 
 � ��$	 False��P�  �@��
IsEditable$
 True��. ��� 
 �@ 	IsTabStop$	 False���� $ PART_EditableTextBox��
 � y�$	 Visible=��� $ ContentSite��
 � y�$	 Hidden=�.��� .+���  �� Padding$ 50q�._��  � RowDefinitions �� $��0 ��� $	��90* ��� $��5* � �� $	��40* ��� $	��60* �.+�&   System.Windows.Controls.DataGrid  -     Name$ Table��$	��200�� �� SelectedIndex$ 0��    CanUserAddRows$  False��!   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$! * $��1��$��0��"   AutoGenerateColumns$" False��$# �� IsSynchronizedWithCurrentItem$# FalseY�$   SelectionUnit$$ FullRow=�$��1,1,1,1q�%   
IsReadOnly$
% True��$��20(�$��Calibri)�
�������$��Center=�&   SelectionMode$& Single=�'   CanUserDeleteRows$' False��(   CanUserResizeRows$( False��)   RowDetailsVisibilityMode$) 	Collapsed=�*   AreRowDetailsFrozen$
* True��"+   VerticalScrollBarVisibility$+ Hidden=�,   HeadersVisibility$, Column=�$-   HorizontalScrollBarVisibility$- Hidden=�.   MinRowHeight$. 30Q�/   GridLinesVisibility$/ 
Horizontal=�0   HorizontalGridLinesBrush
0 �����$��Center=����� *List+1 �� SelectedItem1 �� *SelectedItem+��Z� ��Z� 2   ColumnHeaderStyle2 �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .��� 
 � ��	 �	#FF0C2A4C�� 
 � ��
	 ������� 
 � ��$	 30(��� 
 � ��$	 Center=�3   Columns3 0  *System.Windows.Controls.DataGridTextColumn  ,	  &System.Windows.Controls.DataGridColumn4 	  Width$4 * 5 	  Header$5 Фамилии��1
  +System.Windows.Controls.DataGridBoundColumn6 
  Binding6 �� $
 Name�  $4 50 $5 И.О.��6 �� $ LastName�7   RowStyle7 �� )  #System.Windows.Controls.DataGridRow"  .��� 
 � ��	 �	#FF0C2A4C�� 
 � ��
	 ������� 
 � ��
	 ������� 
 � ��$	 0q��� 
 � ��$	 Center=��� 
 � ��$	 24(�8 �� Triggers8 P�  �@J�IsMouseOver$
 True��. ��� 
 � ��#	 [ 9   	CellStyle9 �� *  $System.Windows.Controls.DataGridCell"  8 P�  �@ 
IsSelected$
 True��. ��� 
 � ��
	 ������� 
 � ��$		 5 5q��� 
 � ��
	 ������� 
 � ��$	 0q��� 
 � ��  MainBlueMiddleGentleColor#	 [ �� $��2��  	BorderAdd#��[ $��30��._��� $��16(�#��[ $��Center=�$��Center=�$��Добавить��*  $Microsoft.Xaml.Behaviors.Interaction:   Triggers: +  %Microsoft.Xaml.Behaviors.EventTrigger  ;   	EventName$; 	MouseDown��*  $Microsoft.Xaml.Behaviors.TriggerBase<   Actions.< 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  =   Command= �� *	AddPrepod+�� $
��30 0q�$��1��$��1��.+��� $��26(�#��[ $!��Преподаватели:��$��Center=�� $��0,10,0,0q� �� $��* ��� $��70 �.+��� -   > ��Name$> TextBoxRoom��$��22(�? �� 	UndoLimit$? 20��@ �� SelectionBrush#@ �  #���  #���  #���  $��30������ *SelectedItem.Name+A �� UpdateSourceTrigger$A PropertyChanged=��� B �� TextAlignment$B Center=�$��10 0 0 0q�$��1��$��22(�$? 20��#@ �  #���  #���  #���  $��30������ *SelectedItem.LastName+$A PropertyChanged=�� $
��0 20q� �� �� .+��� $��0��$��30��#��[ ._��� $��16(�#��[ $��Center=�$��Center=�$��Удалить��:   $; 	MouseDown��.<   = �� *DeletePrepod+�� $��1��#��[ $��30��._��� $��16(�#��[ $��Center=�$��Center=����� *SelectedItem.ID+C �� 	Converter#C [  :   $; 	MouseDown��.<   = �� *	PutLesson+�� y��� *SelectedItem.ID+#C [ .+��� $��0 20 0 0q�#��[ $��Center=�$��Center=�$��22(�$&�� Список предметов:���� -   D ��Name$D ListTeacherLessons��$��Stretch=���Z� ���� *TeacherLessons+��Z� ���� .R��� $
��Auto��$��18(�$
��Wrap=�#��[ ���� *Lesson+�� $��1��#��[ $��30��._��� $��16(�#��[ $��Center=�$��Center=�$#��Удалить предмет��:   $; 	MouseDown��.<   = �� *DeleteLesson+E   CommandParameterE �� F �� ElementName$F ListTeacherLessons��$ SelectedItem��� -   $��LessonsComboBox��$
��0 10q����� *	Lessons+���� .R��� $��16(����� *Lesson+�� $��1��#��[ $��30��._��� $��16(�#��[ $��Center=�$��Center=�$%��Добавить предмет��:   $; 	MouseDown��.<   = �� *	AddLesson+E �� $F LessonsComboBox��$ SelectedItem�