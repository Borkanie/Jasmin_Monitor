   M S B A M L   `   `   `  ����I  EJasmin Monitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Main_BarMain_Bar  5   	       Main_Bar.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Main_Bar     5   	     � Title$  LoginWindow��5   	   $	��300��6   $	��400��6*    � WindowStyle$
 None=�66    J� Opacity$	 0.5Q�6I   ���	#FF706F6F � WindowStartupLocation$ CenterScreen=�6n   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��400 �6   6-    � RowDefinitions 5   
   �� 5      $��90 �6   6*   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��50 �6   6*   .+�5   
   A� -   5         A�Name$ picture��5      $��60��6#   $��60��6.   
����  �$	��Top=�6F   6`   �� 5   
   $��login��6   $��20(�6!   $��
ExtraBlack"�6/   $��0��6G   $��0��6W   $��Bottom=�6d   $��Center=�6   6�   �� 5   
   $��Username��6   $��20(�6$   $��1��62   $��0��6?   $��Center=�6O   $��Center=�6j   $
��Bold"�6�   6�   �� 5   
   -   5         ��Name$ txtbox_Username��5      $��2��6+   $��0��68   $	��250��6H   $��30��6T   $��20(�6`   $��Center=�6n   $��Center=�6�   6�   �� 5   
   $��Password��6   $��20(�6$   $��3��62   $��0��6?   $��Center=�6O   $��Center=�6j   $
��Bold"�6�   6�   G� 5   
   -   5         G�Name$ txtbox_Password��5      $��4��60   $��0��6=   $	��250��6M   $��30��6Y   $��20(�6e   $��Center=�6s   $��Center=�6�   �� 5   
   -   5        	 ��Name$	 btn_done��5      $
��Done��6#   $��20(�62   $��DemiBold"�6@   $��0��6V   $��5��6f   $��Center=�6s   $��Center=�6�   $��80��6�   $��40��6�   
�������$��
245,2,75,8q�6�   6  �� 5   
   -   5        $	 
btn_cancel��5      $��Cancel��6%   $��20(�66   $��DemiBold"�6D   $��0��6Z   $��5��6j   $��Center=�6w   $��Center=�6�   $��80��6�   $��40��6�   
�������$��
75,2,245,8q�6�   6
  5       5!      