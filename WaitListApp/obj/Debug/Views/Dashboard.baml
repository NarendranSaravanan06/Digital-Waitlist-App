   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	    WaitListApp!   WaitListApp.Views.Dashboard   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  	Dashboard��5   	   $	��600��5   	   $
��1000��5   	   ���#1e1e2f
�������$��Segoe UI)�5
   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0,0,0,10q�6!   $��Center=�63   $	��910��6P   .+�5      �� -   5        $	��Add��5      $	��100��6#   $��5q�6/   $��35��6:   ���#4CAF50
�������6n   �� 5      -   5        $
��Edit��5      $	��100��6$   $��5q�60   $��35��6;   ���#FFC107
���   �6o   �� 5      -   5        $��Update��5      $	��100��6&   $��5q�62   $��35��6=   ���#2196F3
�������6q   �� 5      -   5        $��Delete��5      $	��100��6&   $��5q�62   $��35��6=   ���#F44336
�������6q   �� 5      -   5        $��Refresh��5      $	��100��6'   $��5q�63   $��35��6>   ���#9C27B0
�������6r   �� 5      -   5        $��View History��5      $	��120��6,   $��5q�68   $��35��6C   
���� K�
�������6v   �� 5      -   5        $��Logout��5      $	��100��6&   $
��50,5q�62   $��35��6@   ���#607D8B
�������6t   5      &   System.Windows.Controls.DataGrid  5    
   -   5          Name$ 
dgWaitlist��5       $��1��5!         AutoGenerateColumns .     HeadersVisibility$ Column=�5#         CanUserAddRows .  
�������
���   �   AlternatingRowBackground �#f1f1f1
�������   SelectionMode$ Single=�5)      $��14(�5*      	   RowStyle	 5,      �� 5-      
 �� 
TargetType)  #System.Windows.Controls.DataGridRow"
  6    �� Triggers 5.      �� 5/       �� Binding 6&   �� *
IsModified+ �� Value True6E   .a�50      ��  �� Property � �� �� Value
 �����6;   51      53         Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn   Header$	 SNo��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *SNo+6J     58      $
 Name��6%   $ * 6L    63   �� *Name+6L     59      $ Email��6%   $ * 6N    64   �� *Email+6N     5:      $ 	Phone No.��6%   $ * 6T    68   �� *	PhoneNo+6T   4  .System.Windows.Controls.DataGridComboBoxColumn  5;      $ Status��6)   $	 120 6`      SelectedItemBinding 69   �� *Status+   ItemsSource 5<      �� 5=       �� Type" ��6"   .s�5>      ��Waiting��62   ��5@      	Completed��64   ��5A      	Cancelled��64   5B      5D      4  .System.Windows.Controls.DataGridTemplateColumn  5E      $ In Time��6)   $	 100 6:      CellTemplate 5F      �� 5G      .R�5H      �� ��6(   �� *InTime+ �� StringFormat$ hh\:mm��5I      5K        5L      $ Out Time��6)   $	 100 6;    5M      �� 5N      .R�5O      �� ��6(   �� *	OutTime+$ hh\:mm��5P      5R        5S      $
 Date��6%   $	 120 6e      
IsReadOnly .  63   �� *Date+$ 
dd/MM/yyyy��6q   5U      �� 5X   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,10,0,0q�6X   .+�5Y      �� -	   5        $��Previous��5Y      $��5q�6?   �� 5Z      -
   5         ��Name$ txtPageInfo��5Z      $��Center=�6-   $��5q�6H   �� 5[      -   5        $
��Next��5[      $��5q�67   5\      5]      5^      