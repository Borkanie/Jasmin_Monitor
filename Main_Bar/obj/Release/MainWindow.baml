   M S B A M L   `   `   `  ���� D  @WpfAppBar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:WpfAppBar;assembly=WpfAppBar	WpfAppBar  I EJasmin Monitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Main_BarMain_Bar    Main_Bar.MainWindow   2apb*clr-namespace:WpfAppBar;assembly=WpfAppBar  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:Main_Bar     -     � Title$  
MainWindow��$	��120�� � WindowStyle$
 None=� � Topmost .  � 
ResizeMode$ NoResize=����� *1/System.Windows.SystemParameters.WorkArea.Height+����  �� EndPoint$ 1,0.5&� � MappingMode$ RelativeToBoundingBox=� �� 
StartPoint$ 0,0.5&�.���  � Color$ 	#FF530000�� � Offset$ 0.874Q�� $ 	#FFA60707��.��� -   	 �Name$	 	main_Grid��
 � ColumnDefinitions
 �� $	��120 �$	��120�� � RowDefinitions �� $	��65* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��35* ��� $	��75* ��� $	��50* ��� $	��50* ��� $	��65* �.+�A� -    A�Name$ ProfilePicture��$��0��$��0��$��50��$��45�����	#FF1448B4�� -    ��Name$ 	txt_hello��$��Hello��$��0��$��1��$��20(�$
��Bold"�$��Center=�$��Center=��� -   $ txt_girl_name��$��Alissa��$��0��$��2��$��20(�$
��Bold"�$��Center=�$��Center=��� $��3��$��0��
���   �
�������$	��100��$��3�� �� CornerRadius$ 50,50,50,50��$	��Top=�$��Center=��� $��3��$��0��$	��ore��$��Center=�$��Center=�$��20(�$��DemiBold"��� -    ��Name$ Standard_de_inaltime��$��0��$��4��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $ ellipse_ore_actual��$��4��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_ore_actuale��$��4��$��0��$��00��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��0��$��5��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -	   $ ellipse_ore_antecedent��$��5��$��0��
���  ��$��	13,3,91,3q�$��16���� -
   $ txt_ore_antecedent��$��5��$��0��$��00��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��incasari��$��20(�$
��Bold"�$��Center=�$��Center=�$��0��$��6���� $��6��$��0��
���   �
�������$	��100��$��3��$ 50,50,50,50��$	��Top=�$��Center=��� $��0��$��7��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $ ellipsa_incasari_actual��$��7��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_incasari_actual��$��7��$��0��$��0$��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��0��$��8��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $ ellipsa_incasari_anterior��$��8��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_incasari_anterior��$��8��$��0��$��0$��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��
dolari/ora��$��20(�$
��Bold"�$��Center=�$��Center=�$��0��$��9���� $��9��$��0��
���   �
�������$	��100��$��3��$ 50,50,50,50��$	��Top=�$��Center=��� $��0��$��10��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $# ellipsa_incasari_medii_actual��$��10��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_incasari_medii_actual��$��10��$��0��$��0$��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��0��$��11��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $% ellipsa_incasari_medii_anterior��$��11��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $! txt_incasari_medii_anterior��$��11��$��0��$��0$��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��bounce rate��$��20(�$
��Bold"�$��Center=�$��Center=�$��0��$��12���� $��12��$��0��
���   �
�������$	��100��$��3��$ 50,50,50,50��$	��Top=�$��Center=��� $��0��$��13��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $  ellipsa_bounce_rate_actual��$��13��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_bounce_rate_actual��$��13��$��0��$��0%��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��0��$��14��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $" ellipsa_bounce_rate_anterior��$��14��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_bounce_rate_anterior��$��14��$��0��$��0%��$��14(�$��SemiBold"�$��Center=�$��Center=��� $	��Azi��$��20(�$
��Bold"�$��Center=�$��Center=�$��0��$��15���� $��15��$��0��
���   �
�������$	��100��$��3��$ 50,50,50,50��$	��Top=�$��Center=��� $��0��$��16��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $! ellipsa_azi_incasari_actual��$��16��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_azi_incasari_actual��$��16��$��0��$��0$��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��0��$��17��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $ ellipsa_azi_ore_anterior��$��17��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_azi_ore_anterior��$��17��$��0��$��00:00��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��pauza��$��20(�$��DemiBold"�$��Center=�$��Center=�$��0��$��18���� $��0��$��19��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $ ellipsa_pauza��$��19��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ 	txt_pauza��$��19��$��0��$��00:00��$��14(�$��SemiBold"�$��Center=�$��Center=��� $��	Cartonase��$��20(�$
��Bold"�$��Center=�$��0��$��20���� $��20��$��0��
���   �
�������$	��100��$��3��$ 50,50,50,50��$	��Top=�$��Center=��� $��0��$��21��$	��100��$��0,3,0,3q�$ 15,15,15,15��
�������A� -   $ ellipsa_cartonase��$��21��$��0��
���  ��$��	13,3,91,3q�$��16���� -   $ txt_cartonase��$��21��$��0��$��0$��$��14(�$��SemiBold"�$��Center=�$��Center=�A� -   $ indicator_traffic_bun��$��22��$��0��$��	30,0,30,0q�$��60��$��60������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF0D91FF��$ 0Q�� $ 	#FF052B7C��$ 1Q��� -     ��Name$ btn_JasminCam��$��0��$��23��$��40��$��40��
���  ���� -!   $ btn_Home��$��0��$��24��$��50��$��50��
�������$��Center=�$��Center=� � 	Resources  �� 
TargetType �� Property �� Value��%:   '��       �� " ��.���  �@��CornerRadius$ 50���� -"   $ btn_Settings��$��0��$��25��$��30��$��30��
�������$	��Top=�$��Center=� ��%:   '��       �� " ��.���  �@��CornerRadius$ 14��