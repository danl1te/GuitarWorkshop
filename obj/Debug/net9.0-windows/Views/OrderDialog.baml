   M S B A M L   `   `   `  ����  GuitarWorkshopApp)    #GuitarWorkshopApp.Views.OrderDialog   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  Добавить заказ��5   	   $	��300��6    $	��400��6-    � 
ResizeMode$ NoResize=�5   	   .��5      �� $��10q�6   .+�5   
   �� $��Статус:��6   �� 5   
   ��6   �� *Status+ �� UpdateSourceTrigger$ PropertyChanged=��� 5	   
   $��Стоимость:��6   �� 5
   
   ��6   �� *Cost+$ PropertyChanged=��� 5   
   $$��Срок выполнения:��6   (  "System.Windows.Controls.DatePicker  5   
      SelectedDate 6   �� *
Deadline+$ PropertyChanged=��� 5   
   $��Гитара:��6   �� 5   
    �� DisplayMemberPath$
 Type��5       �� SelectedValuePath$ Id��5      ��5      �� *	Guitars+ �� SelectedValue 5      �� *SelectedGuitarId+�� 5   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��0,10,0,0q�6J   .+�5      �� -   5        $��OK��5      $��60��6"   $��0,0,5,0q�6-   6>   �� 5      -   5        $��Отмена��5      $��60��6&   61   5      5      5      