   M S B A M L   `   `   `  ����I  EReport Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Report_ManagerReport_Manager  5   	   #    Report_Manager.managementView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Report_Manager     5   	     � Title$  Incoming Requests��5   
   $	��318��6$   $	��973��61   
�������$��Center=�6O    � WindowStartupLocation$ CenterScreen=�6s   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��829* �6   �� 5      $
��137* �6   .+�5   
   �� -   5         ��Name$ requestList��5      $
��Left=�6(   $	��204��6C   $��	10,10,0,0q�6P   $	��Top=�6c   $	��948��6{   $��2��6�    �� View 5      �� 5      .P�5      �� $��Status��6%    �� Width$ 80��6]    �� DisplayMemberBinding 65   �� *status+6]   �� 5      $��Orden de Compra��6%   $	 120��6i    6>   �� *	requestID+6i   �� 5      $��No. de Guía��6%   $	 100��6c    6:   �� *	trackID+6c   �� 5      $��Carrier��6%   $ 80��6_    66   �� *	carrier+6_   �� 5      $��Descripción��6%   $	 120��6h    6:   �� *description+6h   �� 5      $��	Comprador��6%   $ 80��6_    68   �� *buyer+6_   �� 5      $��Departamento��6%   $ 80��6g    6;   �� *
department+6g   �� 5      $��Gastos��6%   $ 80��6a    65   �� *
spentMoney+6a   �� 5      $��No. de archivos��6%   $	 120��6m    6>   �� *filesUploaded+6m   �� 5      $��Fecha Promesa��6%   $	 120��6i    6<   �� *deliverDate+6i   �� 5      $��Fecha de entrega��6%   $	 120��6p    6?   �� *realDeliverDate+6p   5      5      �� 5   
   -   5         ��Name$ sendThem��5      $��Crear Reportes��6#   $
��Left=�6<   $��26.4,245,0,0q�6W   $	��Top=�6m   $	��102��6�   $��1��6�   �� 5    
   -   5        $ goBack��5       $��Salir��6!   $
��Left=�61   $��
10,245,0,0q�6L   $	��Top=�6`   $��75��6x   6�   �� 5!   
   -   5        $ updateTable��5!      $��
Actualizar��6&   $
��Left=�6;   $��400,245,0,0q�6V   $	��Top=�6k   $��75��6�   6�   5"      5#      