   M S B A M L   `   `   `  ����  	Matemeter    Matemater.PlayerSelection   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5	   	   @d2http://schemas.microsoft.com/expression/blend/2008     5
   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � ShowInTaskbar  .   � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	   
������� � WindowStartupLocation$ CenterScreen=�5   	    � AllowsTransparency .  $��50px, 50px, 50px, 50pxq�5   	    � SizeToContent$ Height=�5   	   $��Comic Sans MS)�5   	    � Icon$! .\Resources\Images\logo.png��5   	    � Title$ 	Matemeter��5   	   $	��850��6   $	��500��6(   $	��500��65   $	��850��6E   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��4* �6   .+�5   
   �� 	 �� Orientation$	 
Horizontal=�6   $��0��6.   $��Center=�6;   $��Center=�6X   .+�5      �� -   5        $��imgLogo��5      $
��70px��6#   $
��70px��60   $��	20px 10pxq�6>   �� 5      $��	Matemeter��5      $��70(�5      $
��Bold"�5      
���  ��$��Center=�5       $��Center=�5!      5"      � 5$   
   $��1��6   
 � ColumnDefinitions
 5%      �� 5&      $��2* �6#   �� 5'      $��1* �6#   .+�5*      &   System.Windows.Controls.DataGrid  -   5        $��	usersGrid��5,         AutoGenerateColumns .  $��0q�5+         CanUserAddRows .  $��0��5.      $
��16px(�5/      
�������$
��Auto=�52      $��430px��53      $��290px��6!   ��50      �� *	Players+ � 	Resources 55      )  #System.Windows.Controls.DataGridRow �� 
TargetType �� Property �� Value:  4System.Windows.Controls.Primitives.DataGridRowHeader=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
ResourceId �� TypeInTargetAssembly��%�  '        '  J     '  �     (�� �     5?   9   $" DataGridSelectAllButtonStyle��"  )�� 56      "  6   .�57      ��  � ��
 �����67   58      �� 59      "  6   .�5:      ��  � ��
 �����67   5;      �� 5<      "  6   .�5=      ��  � ��
 �����67   5>      �� 5?      " ��6   .�5@      ��  � ��
 �   �67   5A      5B         Columns 5D      0  *System.Windows.Controls.DataGridTextColumn  5E      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ Imię��5F   )      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 5H   )   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5G   )   �� *Name+5H   )     5I       . $ Najlepszy wynik��5J   )   $ 1* 5L   )    5K   )   �� *	BestScore+5L   )   5N      �� 5P      $	 Vertical=�6   $��1��60   $
��Left=�5Q      $	��Top=�5R      .+�5T      �� $��Podaj imię: ��6   $��Center=�6/   $
��16px(�6S   $��0px 30px 0px 0pxq�6c   �� 5V      -   5        $��userName��5V      $��150px��5W      $��0q�5X      $
��16px(�5Y      $��Center=�5Z      $��Center=�5[      �� 5]      
�������$	��2pxq�6-    �� CornerRadius$ 
5, 5, 5, 5��6C   $
��10pxq�6]    J� Effect 5^      3	  -System.Windows.Media.Effects.DropShadowEffect	  5_       	  ShadowDepth$ 3Q�6+   ._�5a      �� -   5        $��newUser��5b      $��1��5a      $
��15px(�5c      $��0q�5d      
�������$��Dodaj użytkownika��5f      $��150px��5g      $
��50px��6#   5h      5i      �� 5k      
�������$	��2pxq�6-   $ 
5, 5, 5, 5��6C   $��10px 40px 10px 10pxq�6]    5l      	  5m      $ 3Q�6+   ._�5o      �� -   5        $��start��5p      $��2��5o      
�������$��0q�5r      $��20(�5s      $��START��5t      $��150px��5u      $
��50px��6#   5v      5w      �� 5x      
�������$	��2pxq�6-   $ 
5, 5, 5, 5��6C   $
��10pxq�6]    5y      	  5z      $ 3Q�6+   ._�5|      �� -   5        $��close��5}      $��2��5|      
�������$��0q�5      $��20(�5�      $��WYJDŹ��5�      $��150px��5�      $
��50px��6#   5�      5�      5�      5�      5�      