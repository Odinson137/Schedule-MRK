   M S B A M L   `   `   `  ���� H  DSasha Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Sasha_Project.Views.SettingsViews!Sasha_Project.Views.SettingsViews  _4clr-namespace:Sasha_Project.ViewModels.SettingsPages&Sasha_Project.ViewModels.SettingsPages  O,clr-namespace:Sasha_Project.Views.ConvertersSasha_Project.Views.Converters  ;    5Sasha_Project.Views.SettingsViews.SettingsLessonsView   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Alocal/clr-namespace:Sasha_Project.Views.SettingsViews     K
viewmodels4clr-namespace:Sasha_Project.ViewModels.SettingsPages     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  =
converters,clr-namespace:Sasha_Project.Views.Converters    � DataContext  D  >Sasha_Project.ViewModels.SettingsPages.SettingLessonsViewModel   � 	Resources >  8Sasha_Project.Views.Converters.ChangeToSaveTextConverter   ButtonClickText.  (Sasha_Project.Views.Converters.KursColor  	KursColor��%   &	        &	          .��� .+���  �� Padding$ 50q�._��  � RowDefinitions �� $��5* ��� $	��90* ��� $��5* � � ColumnDefinitions �� $	��40* ��� $	��60* �.+�&   System.Windows.Controls.DataGrid  -     Name$ Table�� �� SelectedIndex$ 0��   CanUserAddRows$ False��$	��0 0q�   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * $��1��$��0��	   AutoGenerateColumns$	 False��$
 �� IsSynchronizedWithCurrentItem$
 FalseY�   SelectionUnit$ FullRow=�$��1,1,1,1q�   
IsReadOnly$
 True��$��20(�$��Calibri)�
�������$��Center=�   SelectionMode$ Single=�   CanUserDeleteRows$ False��   CanUserResizeRows$ False��   RowDetailsVisibilityMode$ 	Collapsed=�   AreRowDetailsFrozen$
 True��"   VerticalScrollBarVisibility$ Hidden=�   HeadersVisibility$ Column=�$   HorizontalScrollBarVisibility$ Hidden=�   MinRowHeight$ 30Q�   GridLinesVisibility$ 
Horizontal=�   HorizontalGridLinesBrush
 �����$��Center=����� *List+ �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem �� *SelectedItem+��Z� ��Z�    ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value �	#FF0C2A4C��  � ��
 �������  � ��$ 30(���  � ��$ Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Предметы:��1	  +System.Windows.Controls.DataGridBoundColumn  	  Binding  �� ! �� Path$! Lesson�"   RowStyle" �� )
  #System.Windows.Controls.DataGridRow" 
 .���  � �� �	#FF0C2A4C��  � ��
 �������  � ��
 �������  � ��$ 0q���  � ��$ Center=���  � ��$ 24(�# �� Triggers# P� $ P� Property$ �@J�IsMouseOver% P� Value$
% True��. ���  � ��  MainBlueGentleColor# [ &   	CellStyle& �� *  $System.Windows.Controls.DataGridCell"  # P� $ �@ 
IsSelected$
% True��. ���  � ��
 �������  � ��$	 5 5q���  � ��
 �������  � ��$ 0q���  � ��  MainBlueMiddleGentleColor# [ �� $��2��  	BorderAdd#��[ $��30��._��� $��16(�  	FontColor#��[ $��Center=�$��Center=�$��Добавить��*  $Microsoft.Xaml.Behaviors.Interaction'   Triggers' +  %Microsoft.Xaml.Behaviors.EventTrigger  (   	EventName$( 	MouseDown��*  $Microsoft.Xaml.Behaviors.TriggerBase)   Actions.) 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  *   Command* �� *	AddLesson+�� $��1��$��1��$
��50 5q�.+��� $��26(�#��[ $��Кабинет:��$��Center=��� -   + ��Name$+ TextBoxRoom��$��22(�, �� 	UndoLimit$, 30��- �� SelectionBrush#- �  #���  #���  #���  $��30��$��0,10,0,0q����� *SelectedItem.Lesson+$ PropertyChanged=�� $��0,10,0,0q� �� �� .+��� $��22(�$, 10��#- �  #���  #���  #���  ���� *SelectedItem.Shorts+$ PropertyChanged=��� $��1��$��22(�#��[ . �� TextAlignment$. Center=�$��Center=�$��Сокращение��� $
��0 10q�$��50�� �� �� �� �� .+��� $��0������ *SelectedItem.Kurs1+/ �� 	Converter#/ [ ._��� $��20(�#��[ $��Center=�$��Center=�$��
Курс 1��'   $( 	MouseDown��.)   0   CommandParameter$0 1��* �� *
ChangeKurs+�� $��1������ *SelectedItem.Kurs2+#/ [ ._��� $��20(�#��[ $��Center=�$��Center=�$��
Курс 2��'   $( 	MouseDown��.)   $0 2��* �� *
ChangeKurs+�� $��2������ *SelectedItem.Kurs3+#/ [ ._��� $��20(�#��[ $��Center=�$��Center=�$��
Курс 3��'   $( 	MouseDown��.)   $0 3��* �� *
ChangeKurs+�� $��3������ *SelectedItem.Kurs4+#/ [ ._��� $��20(�#��[ $��Center=�$��Center=�$��
Курс 4��'   $( 	MouseDown��.)   $0 4��* �� *
ChangeKurs+� $
��0 20q� �� �� .+��� $��0��$��30��#��[ ._��� $��16(�#��[ $��Center=�$��Center=�$��Удалить��'   $( 	MouseDown��.)   * �� *DeleteLesson+�� $��1��#��[ $��30��._��� $��16(�#��[ $��Center=�$��Center=����� *SelectedItem.ID+#/ [  '   $( 	MouseDown��.)   * �� *	PutLesson+0 �� 1 �� ElementName$1 TextBoxRoom��$
! Text�