   M S B A M L   `   `   `  ����  Marketer.Desktop-    'Marketer.Desktop.Pages.OrderDetailsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  OrderDetailsPage��5      $	��450��6    $	��800��6-    � 	Resources 5	       �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell �� Triggers P� Property P� Value��%^  '��       '  �     '  �    '  �    '�� �    �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 5q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$	 200��6)   5      �� 5   
   "  6   .�5      ��  �@ AutoGenerateColumns$ False��64   �� 5       �@ CanUserAddRows$ False��6/   �� 5       �@ CanUserResizeRows$ False��62   �� 5       �@ SelectionMode$ Single=�6.   �� 5       �@ SelectionUnit$ FullRow=�6.   �� 5        �@ AlternatingRowBackground# � ��69   �� 5       �@ GridLinesVisibility$ 
Horizontal=�64   �� 5      # �@ VerticalScrollBarVisibility$
 Auto=�6<   �� 5      % �@ HorizontalScrollBarVisibility$
 Auto=�6>   �� 5       �@ RowBackground
 �����6.   �� 5       �@ ColumnHeaderHeight$ 40Q�63   �� 5       �@ 	RowHeight$ 35Q�6*   �� 5        � ��$ 10q�6'   �� 5!       � ��$ 14(�6)   5"      �� 5%   
   "  6   .�5&      ��  � �� �	#FF6200EE6+   �� 5'       � ��
 �����6+   �� 5(       � ��$ 16(�6)   �� 5)       � ��$ 40��6'   �� 5*       � ��$ Center=�6;   �� 5+       � ��$ Center=�69   5,      �� 5/   
   "  6   .�50      ��  � ��$ 8q�6(    51      P� 52       �@ 
IsSelected$
 True��60   . �53      ��  � �� �	#FFAA00FF63   �� 54       � ��
 �����63   55      57      �� 5:   
   " ��6   .�5;      ��  � ��$ 5q�6'   �� 5<       � ��$ 14(�6)   5=      5>      .��5@      � $��10q�6    � RowDefinitions 5A   
   �� 5B      $
��Auto �6   �� 5C      $��* �6   �� 5D      $��* �6   	 � ColumnDefinitions	 5F   
   �� 5G      $��* �6   �� 5H      $��* �6   .+�5L   
   �� $��	Customer:��6   $��16(�6%   $��Center=�63   $��5q�6N   �� 5M   
   -   5        
 ��Name$
 CustomerComboBox��5M      $��0��6-   $��1��6:   $��5q�6J    �� DisplayMemberPath$ FullName��5N       �� SelectedValuePath$ Id��60   $��30��5O   =   ��5M   U   �� *	Customers+ �� SelectedItem 5O      �� *SelectedCustomer+6=     5R   
   -   5        $��1��5R      $��2��6    $��0��64      AutoGenerateColumns$ False��6c      SelectionMode$ Single=�5S   :   $��5q�6Q   ��5R   D   �� *Orders+ 5S      �� *SelectedOrder+   Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn   Header$ Order ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6N      
IsReadOnly$
 True��6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Id+6X     5W      $ 
Order Date��6%   $ * 6Z   $
 True��6d    69   �� *CreationDate+6d     5X      $ Number of Products��6%   $ * 6d   $
 True��6n    6A   �� *Products.Count+6n   5Z        5]   
   $��2��6   $��2��6    $��0��64   $ False��6e      HeadersVisibility$ Column=�5^      $��5q�6.   ��5]   D   �� *
Products+ 5_        5`      $ Product Name��6%   $ * 6T   $
 True��6^    6;   �� *Name+6^     5a      $ Product Price��6%   $ * 6V   $
 True��6`    6<   �� *Price+6`   5c      5d      5e      