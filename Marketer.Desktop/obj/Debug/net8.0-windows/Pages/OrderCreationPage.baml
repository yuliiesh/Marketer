   M S B A M L   `   `   `  ����  Marketer.Desktop.    (Marketer.Desktop.Pages.OrderCreationPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  OrderCreationPage��5      $	��350��6!   $	��800��6.    � 	Resources 5	       �� 
TargetType �� Property �� Value��%�  '��       '�� �     �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 5q�6(   �� 5       � ��$ 14(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 5q�6(   �� 5       � ��$ 14(�6)   �� 5       � �� �	#FF6200EE6+   �� 5       � ��
 �����6+   5      5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5       $
��Auto �6    � ColumnDefinitions 5"   
   �� 5#      $��2* �6   �� 5$      $��* �6   .+�5'   
   �� $��	Customer:��6   $��16(�6%   $��Center=�63   $��0��6N   $��0��6[   $��5q�6k   �� 5(   
   -   5         ��Name$ CustomerComboBox��5(      $��0��6-   $��1��6:   $��5q�6J   $��30��6U    �� DisplayMemberPath$ FullName��5)   5   	 �� SelectedValuePath$	 Id��6R   ��6   �� *	Customers+
 �� SelectedItem
 5*      �� *SelectedCustomer+�� 5,   
   $��Product Name:��6   $��16(�6)   $��Center=�67   $��1��6R   $��0��6_   $��5q�6o   �� 5-   
   $��1��6T   $��1��6a   $��5q�6q   $��30��6|   ��6   �� *ProductName+ �� UpdateSourceTrigger$ PropertyChanged=�6|   �� 5/   
   $��Product Price:��6   $��16(�6*   $��Center=�68   $��2��6S   $��0��6`   $��5q�6p   �� 50   
   $��2��6U   $��1��6b   $��5q�6r   $��30��6}   ��6   �� *ProductPrice+$ PropertyChanged=�6}   �� 53   
   $��3��64   $��2��6A   $��0��6U   $��5q�6e   ��6   �� *
Products+ �� View 54      �� 55      .P�56      �� $��Product Name��6%    �� DisplayMemberBinding 6;   �� *Name+�� 57      $��Price��6%    64   �� *Price+58      5:      �� 5<   
   $��Add Product��6   $��4��6M   $��0��6Z   $	��120��6j   ��6'   �� *AddProductCommand+6j   �� 5=   
   $��Create Order��6   $��4��6O   $��1��6\   $	��120��6l   ��6(   �� *CreateOrderCommand+6l   5>      5?      