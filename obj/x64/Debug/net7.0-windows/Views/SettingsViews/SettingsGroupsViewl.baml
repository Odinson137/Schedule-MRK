   M S B A M L   `   `   `  ����H  DSasha Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Sasha_Project.Views.SettingsViews!Sasha_Project.Views.SettingsViews  5   	   _4clr-namespace:Sasha_Project.ViewModels.SettingsPages&Sasha_Project.ViewModels.SettingsPages  5   	   O,clr-namespace:Sasha_Project.Views.ConvertersSasha_Project.Views.Converters  6�   :    4Sasha_Project.Views.SettingsViews.SettingsGroupsView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:Sasha_Project.Views.SettingsViews     5   	   K
viewmodels4clr-namespace:Sasha_Project.ViewModels.SettingsPages     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  6Q   =
converters,clr-namespace:Sasha_Project.Views.Converters  6�     � DataContext  5
      D  >Sasha_Project.ViewModels.SettingsPages.SettingsGroupsViewModel  5   
    � 	Resources 5      >  8Sasha_Project.Views.Converters.ChangeToSaveTextConverter   ButtonClickText<  6Sasha_Project.Views.Converters.NewPrepodAddItemVisible  Visible �� 
TargetType   ComboBoxToggleButtonTemplate � ColumnDefinitions  MainBlueColorMiddle �� CornerRadius  MainBlueGentleColor �� Triggers P� Property P� Value �� 
TargetName  MainBlueColorBlack	 �� Value
 �� Property �� 
TargetType  	FontColor �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� 
IsReadOnly �� 
IsReadOnly � AllowsTransparency �� IsDropDownOpen J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation��%6  &	        &	      &	 &     7  7  7  7  7  7  7  7  7  7  7  7  7  '�� `    7  7  7  7  7  7    5   
   6/     5   
   6-   �� 5   
   " d�6   .R�5      �  5      �� 5      �� 5      $��20 �6'   .+�5      �� $��Border��6-   $��2��6   8��  $ 5 5 0 0��5      $��
1, 1, 1, 1q�6&   8�� 5      �� 5      $��ButtonBorder��6�   $��1��6   $ 0 0 5 0��6)   $��
1, 1, 1, 1q�6@   8�� 8�� 5      F� 5      $��Arrow��6   $��1��6$   =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=�5!      8�� $��Center=�5"      5#       5$      P� 5%       �@J�IsMouseOver$
 True��6;   . �5&      �� $ ButtonBorder��69   8	  
 � ��6S   5'      P� 5(       �@d�	IsChecked$
 TrueY�6<   . �5)      �� $ ButtonBorder��69   8	  
 � ��6S   �� 5*      $ Arrow��63   8	  
 � ��6F   5+      P� 5,       � {�$ False��69   . �5-      �� $ Border��69   8	  
 � ��6M   �� 5.      $ ButtonBorder��69   8	 	 
 � ��6S   �� 5/      $ ButtonBorder��6;   8	 
 
 � ��6U   �� 50      8	  
 � ��6?   �� 51      $ Arrow��63   8	  
 � ��6F   52      54      �� 56   
   " ��6   .�57      �� 
 �@J�SnapsToDevicePixels$
	 True��6>   �� 58      
 �@�OverridesDefaultStyle$
	 True��6G   �� 59      
 � ��$	 Hidden=�6K   �� 5:      
 � ��$	 Hidden=�6I   �� 5;      
 � ��$
	 True��6>   �� 5<      
 � ��8	   67   �� 5=      
 �@�FocusVisualStyle	 6B   Z� �� 5>      
 � ��	 5?      �� 5@      " ��6&   .R�5A      � .+�5B      d� $��ToggleButton��6+   $��2��6?   $ Press=�5C      $|�False��6#   8��  5D      �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=�5E      �� 5G      $��ContentSite��6/   $��5, 3, 23, 3q�6B   $ False��6W   $
��Left=�5H      $��Center=�6:   #��z 5I      #��z 5J      #��z��5K      �� 5L      $��PART_EditableTextBox��6&   $��3, 3, 23, 3q�6B   # z 5M   "   $y�Hidden=�5N   "   8�� $
��Left=�5O   "   $��Center=�6=   $��30��6X   $��24(�6d   $
|�True��5P   "   .��64   ��5Q   "   �� 5R   &   " ��66   .R�5S   *   �� $��PART_ContentHost��61   $|�False��6I   5T   '   .��5U   4   5V      � 5X      $��Popup��6$   $��Bottom=�61   $|�False��5Y      $
 True��6&   #��z 5Z      $��Slide=�5[      .��5\   "   � $��DropDown��6'   $
 True��67   #��z 5]      #��z 5^      .+�5_   &   �� $��DropDownBorder��6-   8�� $��
0, 1, 0, 0q�6u   $ 0��5`      $��1,1,1,1q�6*   8�� 5a      �� 5b   &   $��4q�63   $
 True��6>   .��5c   *   �� $ 	Contained=�69   5d   '   5e   #   5f      5g       5h      P� 5i       �@��HasItems$ False��6G   . �5j   "   �� $ DropDownBorder��6O   
 � ��$		 500��6k   5k      P� 5l       � {�$ False��6E   . �5m   "   �� 8	  
 � ��6K   5n      P� 5o       �@��
IsGrouping$
 True��6I   . �5p   "   �� 
 � ��$	 False��6R   5q      P� 5r       �@��
IsEditable$
 True��6E   . �5s   "   �� 
 �@ 	IsTabStop$	 False��6Q   �� 5t   "   $ PART_EditableTextBox��6I   
 � y�$	 Visible=�6k   �� 5u   "   $ ContentSite��6I   
 � y�$	 Hidden=�6b   5v      5x      5z      5{      5|      .��5~      � .+�5   
   ��  �� Padding$ 50q�6   ._�5�      �  � RowDefinitions 5�      �� 5�      $��0 �6$   �� 5�      $	��90* �6$   �� 5�      $��5* �6$    5�      �� 5�      $	��40* �6'   �� 5�      $	��60* �6'   .+�5�      &   System.Windows.Controls.DataGrid  -   5          Name$ Table��5�   A   $	��200��5�       �� SelectedIndex$ 0��6*       CanUserAddRows$  False��6<   !   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$! * 6S   $��1��5�      $��0��6(   "   AutoGenerateColumns$" False��68   $# �� IsSynchronizedWithCurrentItem$# FalseY�5�      $   SelectionUnit$$ FullRow=�5�   2   $��1,1,1,1q�6J   %   
IsReadOnly$
% True��6\   $��20(�5�      $��Calibri)�6)   
�������$��Center=�5�      &   SelectionMode$& Single=�5�      '   CanUserDeleteRows$' False��5�      (   CanUserResizeRows$( False��65   )   RowDetailsVisibilityMode$) 	Collapsed=�5�      *   AreRowDetailsFrozen$
* True��6@   "+   VerticalScrollBarVisibility$+ Hidden=�5�      ,   HeadersVisibility$, Column=�6@   $-   HorizontalScrollBarVisibility$- Hidden=�5�      .   MinRowHeight$. 30Q�6B   /   GridLinesVisibility$/ 
Horizontal=�6T   0   HorizontalGridLinesBrush
0 �����$��Center=�5�   <   ��5�      �� *Groups+1 �� SelectedItem1 6:   �� *SelectedItem+��5�      Z� ��5�   8   Z� 2   ColumnHeaderStyle2 5�      �� 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5�      �� 
 � ��	 �	#FF0C2A4C6;   �� 5�      
 � ��
	 �����6;   �� 5�      
 � ��$	 30(�69   �� 5�      
 � ��$	 Center=�6K   5�      3   Columns3 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,	  &System.Windows.Controls.DataGridColumn4 	  Width$4 * 6-   5 	  Header$5 Группы��67   1
  +System.Windows.Controls.DataGridBoundColumn6 
  Binding6 6G   �� $ Title�7   RowStyle7 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6    .�5�      �� 
 � ��	 �	#FF0C2A4C6;   �� 5�      
 � ��
	 �����6;   �� 5�      
 � ��
	 �����6<   �� 5�      
 � ��$	 0q�6@   �� 5�      
 � ��$	 Center=�6K   �� 5�      
 � ��$	 24(�69   8 �� Triggers8 5�      P� 5�   "    �@J�IsMouseOver$
 True��6A   . �5�   &   �� 
 � ��#	 [ 6C   5�   #   5�      9   	CellStyle9 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6    8 5�      P� 5�   "    �@ 
IsSelected$
 True��6@   . �5�   &   �� 
 � ��
	 �����6C   �� 5�   &   
 � ��$		 5 5q�6@   �� 5�   &   
 � ��
	 �����6D   �� 5�   &   
 � ��$	 0q�6H   �� 5�   &   
 � ��  MainBlueMiddleGentleColor#	 [ 6C   5�   #   5�      5�      �� 5�      $��2��6     	BorderAdd#��[ 6&   $��30��6I   ._�5�      �� $��16(�6    #��[ 6.   $��Center=�5�   ,   $��Center=�6I   $��Добавить��6d   *  $Microsoft.Xaml.Behaviors.Interaction:   Triggers: 5�      +  %Microsoft.Xaml.Behaviors.EventTrigger  5�      ;   	EventName$; 	MouseDown��6)   *  $Microsoft.Xaml.Behaviors.TriggerBase<   Actions.< 5�      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  =   Command= 5�   )   �� *
AddGroup+5�      5�      �� 5�      $
��30 0q�6   $��1��6+   $��1��68   .+�5�      �� $��26(�6    #��[ 6.   $��Группа:��6V   $��Center=�6e   �� 5�      -   5        > ��Name$> TextBoxRoom��5�   ,   $��22(�6   ? �� 	UndoLimit$? 20��5�      @ �� SelectionBrush#@ �  6(   #���  6_   #���  5�      #���  6C   $��30��5�      $��0,10,0,0q�6%   A �� TextAlignment$A Center=�67   ��5�      �� *SelectedItem.Title+B �� UpdateSourceTrigger$B PropertyChanged=�5�   7   � 5�      $��0 20 0 0q�6    5�      �� 5�      �� 5�      .+�5�      �� $��Страница:��6$   
�������$��22(�6H   $��Center=�5�   $   �� 5�      $��20(�5�      $��1��6-   ��5�   #   �� *Days+1 6@   �� *SelectedItem.Page+5�      5�      �� 5�      $��22(�6    #��[ 6.   $��Порядок:��6V   $��Center=�6f   �� 5�      $��22(�6   $? 20��5�      #@ �  6(   #���  6_   #���  5�      #���  6C   $��30��5�      $��0,10,0,0q�6%   $A Center=�67   ��5�   ,   �� *SelectedItem.Numeric+5�   7   � 5�      $
��0 20q�6    5�      �� 5�      �� 5�      .+�5      �� $��0��6!   $��30��61   #��[ 6=   ._�5     �� $��16(�6(   #��[ 66   $��Center=�5  ,   $��Center=�6I   $��Удалить��6d   : 5       5  "   $; 	MouseDown��61   .< 5  &     = 5  1   �� *DeleteGroup+5  #   5	     �� 5     $��1��6!   #��[ 61   $��30��6T   ._�5     �� $��16(�6(   #��[ 66   $��Center=�5  (   $��Center=�6E   ��5  (   �� *SelectedItem.ID+C �� 	Converter#C [  : 5       5  "   $; 	MouseDown��61   .< 5  &     = 5  )   �� *
PutGroup+5  #   5     5     5G     5H     5I     5J     5L     