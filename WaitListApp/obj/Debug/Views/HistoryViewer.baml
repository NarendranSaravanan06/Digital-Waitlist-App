   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	    WaitListApp%   WaitListApp.Views.HistoryViewer   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  Waitlist History Viewer��5   	   $	��600��5   	   $
��1000��5   	   ���#1e1e2f
�������$��Segoe UI)�5
   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ]� $��0��6   $��0,0,0,70q�6!   ��.  .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�62   $��0,0,10,0q�6H   $��34��6Z   .+�5      �� $��From:��6   $��Center=�6(   $��0,0,5,0q�6C   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dateFrom��5      $	��120��6/   $��0,0,10,0q�6;   �� 5      $	��To:��6   $��Center=�6&   $��0,0,5,0q�6A     5      -   5        $ dateTo��5      $	��120��6-   $��0,0,10,0q�69   �� 5      -   5        $��Fetch��5      $	��100��6)   $��0,0,10,0q�65   ���#4CAF50
�������6o   5      �� 5      -   5        $��Back to Dashboard��5      $��Right=�61   $	��112��6H   
���� K�
�������$��33��6�   5      �� 5"   
   $��0��6   $ 
Horizontal=�6"   $��Center=�6;   $��	0,50,0,10q�6X   .+�5#      �� -   5        $
��Edit��5#      $	��100��6$   $��35��60   $��10q�6<   ���#FFC107
���   �6p   �� 5$      -   5        $��Update��5$      $	��100��6&   $��35��62   $��10q�6>   ���#2196F3
�������6r   �� 5%      -   5        $��Delete��5%      $	��100��6&   $��35��62   $��10q�6>   ���#F44336
�������6r   5&      &   System.Windows.Controls.DataGrid  5)   
   -   5          Name$ 	dgHistory��5)      $��1��5*         AutoGenerateColumns .     HeadersVisibility$ Column=�5,         CanUserAddRows .  
�������
���   �	   AlternatingRowBackground	 �#f1f1f1
�������
   SelectionMode$
 Single=�52      $��14(�53         RowStyle 55      �� 56       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 57      �� 58       �� Binding 6&   �� *
IsModified+ �� Value True6E   .a�59      ��  �� Property � �� �� Value
 �����6;   5:      5<         Columns 5?      0  *System.Windows.Controls.DataGridTextColumn  5@      ,  &System.Windows.Controls.DataGridColumn   Header$	 SNo��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *SNo+6J     5A      $
 Name��6%   $ * 6L    63   �� *Name+6L     5B      $ Email��6%   $ * 6N    64   �� *Email+6N     5C      $ 	Phone No.��6%   $ * 6T    68   �� *	PhoneNo+6T   4  .System.Windows.Controls.DataGridComboBoxColumn  5D      $ Status��6)   $	 120 6`      SelectedItemBinding 69   �� *Status+   ItemsSource 5E      �� 5F       �� Type" ��6"   .s�5G      ��Waiting��62   ��5I      	Completed��64   ��5J      	Cancelled��64   5K      5M      4	  .System.Windows.Controls.DataGridTemplateColumn	  5N      $ In Time��6)   $	 100 6:    	  CellTemplate 5O      �� 5P      .R�5Q      �� ��6(   �� *InTime+ �� StringFormat$ hh\:mm��5R      5T      	  5U      $ Out Time��6)   $	 100 6;    5V      �� 5W      .R�5X      �� ��6(   �� *	OutTime+$ hh\:mm��5Y      5[        5\      $
 Date��6%   $	 120 6e      
IsReadOnly .  63   �� *Date+$ 
dd/MM/yyyy��6q   5^      �� 5a   
   $��2��6   $ 
Horizontal=�6"   $��Center=�6;   $��0,10,0,0q�6X   .+�5b      �� -	   5        $��Previous��5b      $��5q�6?   �� 5c      -
   5         ��Name$ txtPageInfo��5c      $��Center=�6-   $��5q�6H   �� 5d      -   5        $
��Next��5d      $��5q�67   5e      5f      5g      