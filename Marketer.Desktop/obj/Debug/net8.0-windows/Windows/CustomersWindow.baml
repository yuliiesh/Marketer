   M S B A M L   `   `   `  ����  Marketer.Desktop.    (Marketer.Desktop.Windows.CustomersWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	     � Title$  CustomersWindow��5   	   $	��450��6!   $	��800��6.    � 	Resources 5      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell �� Triggers P� Property P� Value��%,  '        '      '  +    �� 5
   
   "  6   .�5      ��  �@ AutoGenerateColumns$ False��64   �� 5       �@ CanUserAddRows$ False��6/   �� 5       �@ CanUserResizeRows$ False��62   �� 5       �@ SelectionMode$ Single=�6.   �� 5       �@ SelectionUnit$ FullRow=�6.   �� 5        �@ AlternatingRowBackground# � ��69   �� 5       �@ GridLinesVisibility$ 
Horizontal=�64   �� 5      # �@ VerticalScrollBarVisibility$
 Auto=�6<   �� 5      % �@ HorizontalScrollBarVisibility$
 Auto=�6>   �� 5       �@ RowBackground
 �����6.   �� 5       �@ ColumnHeaderHeight$ 40Q�63   �� 5       �@ 	RowHeight$ 35Q�6*   �� 5       � ��$ 10q�6'   �� 5       � ��$ 14(�6)   5      �� 5   
   "  6   .�5      ��  � �� �	#FF6200EE6+   �� 5       � ��
 �����6+   �� 5       � ��$ 16(�6)   �� 5        � ��$ 40��6'   �� 5!       � ��$ Center=�6;   �� 5"       � ��$ Center=�69   5#      �� 5&   
   "  6   .�5'      ��  � ��$ 8q�6(    5(      P� 5)       �@ 
IsSelected$
 True��60   . �5*      ��  � �� �	#FFAA00FF63   �� 5+       � ��
 �����63   5,      5.      5/      .��51      � .+�52   
     ��6   �� *	Customers+ �� SelectedItem 65   �� *SelectedCustomer+	   Columns	 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn
   Header$
 
First Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6W      
IsReadOnly$
 True��6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	FirstName+6a     55      $
 	Last Name��6%   $ * 6U   $
 True��6_    68   �� *
LastName+6_     56      $	
 Age��6%   $ * 6J   $
 True��6T    62   �� *Age+6T     57      $
 Orders Count��6%   $ * 6[   $
 True��6e    6;   �� *OrdersCount+6e     58      $
 Total Products��6%   $ * 6_   $
 True��6i    6=   �� *ProductsTotal+6i     59      $
 Total Price With Discount��6%   $ * 6s   $
 True��6}    6H   �� *TotalPriceWithDiscount+6}    � ContextMenu 5<      �� 5=      .@�5>      m� $��Delete��6    m� Command 5?      �� *DeleteCustomerCommand+ m� CommandParameter 5@      �� *SelectedCustomer+m� 5A      $��Add Discount��6    5B      �� *AddDiscountCommand+ 5C      �� *SelectedCustomer+5D      5F      5G      5H      