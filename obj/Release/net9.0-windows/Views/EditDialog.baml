   M S B A M L   `   `   `  ����   GuitarWorkshopApp(    "GuitarWorkshopApp.Views.EditDialog   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$"  Редактирование��$	��450��$	��400�� � 
ResizeMode$ NoResize=� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value&   System.Windows.Controls.DataGrid ��Name �� 
TargetName   	CardStyle��%�  '��       '  �    '��     &	  �    �� " ��.���  � ��$ 5q���  � ��$	 8,4q���  � ��$ SemiBold"���  � �� �#3a3a5e��  � ��
 �������  � ��$ 0q���  �@�Cursor$
 Hand����  � ��$	 100����  � ��$ Stretch=���  � �� �� " ��.R��� #��z��$ 5��._��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver$

 True��. ���  � �� �#5a5aa0P� 	 � ��$

 True��. ���  � �� �#2a2a4e�� "  .���  � �� �#2a2a45��  �@ RowBackground �#2e2e56��   �@ AlternatingRowBackground �#242447��  � ��
 �������  � �� �#333358��  � ��$ 1q���  � ��$ 10q���  �@ AutoGenerateColumns$ False����  � ��$ Stretch=���  � ��$ Stretch=��� " ��.���  � �� �#242447��  � ��
 �������  � ��$
 Bold"���  � ��$
 10,4q���  � ��$ 2q���  � �� �� " ��.R��� $��Border��#��z��$ 5,5,0,0��$��2q�._��� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,2q� P� 	 �@��
IsSelected$

 True��. ��� $ Border�� � �� �#5a5aa0P� 	 �@J�IsMouseOver$

 True��. ��� $ Border�� � �� �#4a4a90�� " ��.���  � �� �#5a5aa0��  � ��$ 1q���  �@��CornerRadius$ 5����  � ��$ 5q���  �@��Padding$ 10q���  � �� �#2a2a45��  � ��$	 250��.���  � RowDefinitions �� $��* ��� $
��Auto �.+��� $��0��$
��Auto=�.���� -    ��Name$ EditFieldsPanel��$��10q�A� $��1�� A� Rows$ 1�� A� Columns$ 3��$��Right=�$��10q�$	��350��.+��� -   $��Сохранить���� -   $��Удалить���� -   $��Отмена��