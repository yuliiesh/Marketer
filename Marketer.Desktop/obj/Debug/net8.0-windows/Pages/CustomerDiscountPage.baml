   M S B A M L   `   `   `  ����  Marketer.Desktop1    +Marketer.Desktop.Pages.CustomerDiscountPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  CustomerDiscountPage��5      $	��450��6$   $	��800��61    � 	Resources 5      &   System.Windows.Controls.DataGrid �� 
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
 �����63   5,      5.      5/      .��50      � .+�51   
        AutoGenerateColumns$ False��6=   ��6   �� *CustomerDiscounts+	   Columns	 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn
   Header$
 
First Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6`      
IsReadOnly$
 True��6j   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Customer.FirstName+6j     54      $
 	Last Name��6%   $ * 6^   $
 True��6h    68   �� *Customer.LastName+6h     55      $
 Discount��6%   $ * 6T   $
 True��6^    67   �� *
Discount+6^   57      58      59      