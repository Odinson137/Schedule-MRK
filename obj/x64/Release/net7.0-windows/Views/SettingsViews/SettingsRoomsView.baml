   M S B A M L   `   `   `  ���� H  DSasha Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Sasha_Project.Views.SettingsViews!Sasha_Project.Views.SettingsViews  _4clr-namespace:Sasha_Project.ViewModels.SettingsPages&Sasha_Project.ViewModels.SettingsPages  9    3Sasha_Project.Views.SettingsViews.SettingsRoomsView   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Alocal/clr-namespace:Sasha_Project.Views.SettingsViews     K
viewmodels4clr-namespace:Sasha_Project.ViewModels.SettingsPages     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors    � DataContext  C   =Sasha_Project.ViewModels.SettingsPages.SettingsRoomsViewModel  .��� .+���  �� Padding$ 50q�._��  � RowDefinitions �� $��5* ��� $	��90* ��� $��5* � � ColumnDefinitions �� $	��40* ��� $	��60* �.+�&   System.Windows.Controls.DataGrid  -     Name$ Table��$	��250�� �� SelectedIndex$ 0��   CanUserAddRows$ False��$	��0 0q�   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * $��1��$��0��   AutoGenerateColumns$ False��$	 �� IsSynchronizedWithCurrentItem$	 FalseY�
   SelectionUnit$
 FullRow=�$��1,1,1,1q�   
IsReadOnly$
 True��$��20(�$��Calibri)�
�������$��Center=�   SelectionMode$ Single=�   CanUserDeleteRows$ False��   CanUserResizeRows$ False��   RowDetailsVisibilityMode$ 	Collapsed=�   AreRowDetailsFrozen$
 True��"   VerticalScrollBarVisibility$ Hidden=�   HeadersVisibility$ Column=�$   HorizontalScrollBarVisibility$ Hidden=�   MinRowHeight$ 30Q�   GridLinesVisibility$ 
Horizontal=�   HorizontalGridLinesBrush
 �����$��Center=����� *List+ �� Mode$ OneWay=� �� SelectedItem �� *SelectedItem+��Z� ��Z�    ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value �	#FF0C2A4C��  � ��
 �������  � ��$ 30(���  � ��$ Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Кабинеты:��1  +System.Windows.Controls.DataGridBoundColumn   Binding ��   �� Path$  Value�!   RowStyle! �� )  #System.Windows.Controls.DataGridRow"  .���  � �� �	#FF0C2A4C��  � ��
 �������  � ��
 �������  � ��$ 0q���  � ��$ Center=���  � ��$ 24(�" �� Triggers" P� # P� Property# �@J�IsMouseOver$ P� Value$
$ True��. ���  � ��   MainBlueGentleColor# [  %   	CellStyle% �� *	  $System.Windows.Controls.DataGridCell" 	 " P� # �@	 
IsSelected$
$ True��. ���  � ��
 �������  � ��$	 5 5q���  � ��
 �������  � ��$ 0q���  � ��  MainBlueMiddleGentleColor# [ �� $��2��  	FontColor#��[ $��20(����� *
List.Count+& �� StringFormat$& Количество: {0}���� $	��300��$��1��$��1��$
��50 5q�$��Center=�.+��� $��26(�#��[ $��Кабинет:��$��Center=��� -   ' ��Name$' TextBoxRoom��$��22(�( �� 	UndoLimit$( 20��) �� SelectionBrush#) �   #���   #���  #���  $��30��$��0,10,0,0q����� *SelectedItem.Value+� $
��0 20q� �� �� �� .+��� $��0��$��30��  	BorderAdd#��[ ._��� $��16(�#��[ $��Center=�$��Center=�$��Удалить��*
  $Microsoft.Xaml.Behaviors.Interaction* 
  Triggers* +  %Microsoft.Xaml.Behaviors.EventTrigger  +   	EventName$+ 	MouseDown��*  $Microsoft.Xaml.Behaviors.TriggerBase,   Actions., 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  -   Command- �� *
DeleteRoom+�� $��1��#��[ $��30��._��� $��16(�#��[ $��Center=�$��Center=�$��Добавить��*   $+ 	MouseDown��.,   - �� *	AddRoom+.   CommandParameter. �� / �� ElementName$/ TextBoxRoom��$
  Text��� $��2��#��[ $��30��._��� $��16(�#��[ $��Center=�$��Center=�$��Изменить��*   $+ 	MouseDown��.,   - �� *
SaveRoom+. �� $/ TextBoxRoom��$
  Text�