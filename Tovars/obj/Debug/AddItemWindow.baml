   M S B A M L   `   `   `  ����
  Diplom    Diplom.AddItemWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$)  #Добавление продажи��5   	   $	��513��6$   $	��500��61    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   .��5      � $	��Top=�6   $	��503��6$   .+�5   
   �� $)��#Добавление продажи��6   $��Center=�6-   $	��Top=�6J   $��24(�6b   $��Italic$�6p   $
��Bold"�6�   $��0,48,0,0q�6�   $��43��6�   $	��264��6�   6�   �� 5   
   $��Дата��6   $
��Left=�6   $��288,314,0,0q�6:   $	��Top=�6P   $	��120��5      $��14(�6   $
��Bold"�6*    J� RenderTransformOrigin$ 0.459,0.577&�6<   $��28��6`   $��Cambria)�6l   �� 5   
   $��
Товар��6   $��Center=�6    $��	0,114,0,0q�6=   $	��Top=�6P   $��50��6h   $��14(�6s   $
��Bold"�6�   $��27��6�   $��Cambria)�6�   $ 1.379,0.843&�6�   �� 5   
   -   5         ��Name$ tovarBox��5      $��Center=�5      $��	0,161,0,0q�60   $	��Top=�6C   $	��360��6[   $��43��6g   ���	#FF2B65C1��5      �� 5   �   �� 5   
   -   5         ��Name$ dateTxt��5       �� 
IsReadOnly . $
��Left=�65   $��288,360,0,0q�6Q    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $	��166��6�   $��21��6�   
����������	#FF2B65C16�   �� 5   
   -   5         ��Name$ 
appointBtn��5      $��Оформить��6:   $
��Left=�6M   $��
40,404,0,0q�6h   $	��Top=�6|   $	��166��6�   $��43��6�   ���	#FF2B65C1$��Cambria)�6�   $��16(�6�   
����������	#FF2B65C1	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   -   5        $ 	cancelBtn��5      $��Отменить��6<   $
��Left=�6O   $��288,404,0,0q�6j   $	��Top=�6   $	��166��6�   $��43��6�   ���	#FF86B2F1$��Cambria)�6�   $��16(�6�   ���	#FF86B2F1	 5        5      5!      �� 5"   
   $'��!Количество товара��6   $
��Left=�6,   $��
70,230,0,0q�6G   $	��Top=�6\   $	��142��5#      $��14(�6   $
��Bold"�6'   $ 0.459,0.577&�69   $��28��6]   $��Cambria)�6i   �� 5$   
   -   5        $ 	KolichTxt��5$      {�.  $
��Left=�67   $��
70,276,0,0q�6S   $
 Wrap=�6g   $	��Top=�6{   $	��166��6�   $��21��6�   
����������	#FF2B65C16�   �� 5%   
   $)��#Итоговая стоимость��6   $
��Left=�6-   $��288,230,0,0q�6H   $	��Top=�6^   $	��166��5&      $��14(�6   $
��Bold"�6'   $ 0.459,0.577&�69   $��28��6]   $��Cambria)�6i   �� 5'   
   -   5        $ ItogTxt��5'       . $
��Left=�65   $��288,276,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��166��6�   $��21��6�   
����������	#FF2B65C16�   5)      5*      