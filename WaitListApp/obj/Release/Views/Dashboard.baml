   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   WaitListApp!   WaitListApp.Views.Dashboard   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6sys&clr-namespace:System;assembly=mscorlib       � Title$  	Dashboard��$	��600��$
��1000�����#1e1e2f
�������$��Segoe UI)� � WindowStartupLocation$ CenterScreen=�.��� $��20q� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0��$��0,0,0,10q�$��Center=�$	��910��.+��� -   $	��Add��$	��100��$��5q�$��35�����#4CAF50
��������� -   $
��Edit��$	��100��$��5q�$��35�����#FFC107
���   ��� -   $��Update��$	��100��$��5q�$��35�����#2196F3
��������� -   $��Delete��$	��100��$��5q�$��35�����#F44336
��������� -   $��Refresh��$	��100��$��5q�$��35�����#9C27B0
��������� -   $��View History��$	��120��$��5q�$��35��
���� K�
��������� -   $��Logout��$	��100��$
��50,5q�$��35�����#607D8B
�������&   System.Windows.Controls.DataGrid  -     Name$ 
dgWaitlist��$��1��   AutoGenerateColumns .     HeadersVisibility$ Column=�   CanUserAddRows .  
�������
���   �   AlternatingRowBackground �#f1f1f1
�������   SelectionMode$ Single=�$��14(�	   RowStyle	 �� 
 �� 
TargetType)  #System.Windows.Controls.DataGridRow"
   �� Triggers ��  �� Binding �� *
IsModified+ �� Value True.a���  �� Property � �� �� Value
 �����   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 SNo��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *SNo+  $
 Name��$ *  �� *Name+  $ Email��$ *  �� *Email+  $ 	Phone No.��$ *  �� *	PhoneNo+4  .System.Windows.Controls.DataGridComboBoxColumn  $ Status��$	 120    SelectedItemBinding �� *Status+   ItemsSource ��  �� Type" ��.s���Waiting����	Completed����	Cancelled��4  .System.Windows.Controls.DataGridTemplateColumn  $ In Time��$	 100    CellTemplate �� .R��� ���� *InTime+ �� StringFormat$ hh\:mm��  $ Out Time��$	 100  �� .R��� ���� *	OutTime+$ hh\:mm��  $
 Date��$	 120    
IsReadOnly .  �� *Date+$ 
dd/MM/yyyy���� $��2�� �� Orientation$ 
Horizontal=�$��Center=�$��0,10,0,0q�.+��� -	   $��Previous��$��5q��� -
    ��Name$ txtPageInfo��$��Center=�$��5q��� -   $
��Next��$��5q�