   M S B A M L   `   `   `  ���� I  EReport Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Report_ManagerReport_Manager  !    Report_Manager.receivedView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:Report_Manager       � Title$  Incoming Requests��$	��318��$	��973��
�������$��Center=� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $
��829* ��� $
��137* �.+��� -    ��Name$ requestList��$
��Left=�$	��204��$��	10,27,0,0q�$	��Top=�$	��948��$��2�� �� View �� .P��� $��	Autorizar�� �� Width$ 60�� �� CellTemplate �� .R���  d� 	IsChecked �� *requestChecked+�� $��Status��$ 80�� �� DisplayMemberBinding �� *status+�� $��Orden de Compra��$	 120�� �� *	requestID+�� $��No. de Guía��$	 100�� �� *	trackID+�� $��	Comprador��$	 120�� �� *user+�� $��Carrier��$ 80�� �� *	carrier+�� $��Descripción��$	 120�� �� *description+�� $��	Prioridad��$ 80�� �� *buyer+�� $��Departamento��$ 80�� �� *
department+�� $��Gastos��$ 80�� �� *
spentMoney+�� $��No. de archivos��$	 120�� �� *filesUploaded+�� $��Fecha Promesa��$	 120�� �� *deliverDate+�� $��Fecha de Creación��$	 120�� �� *realDeliverDate+�� -   	 ��Name$	 goBack��$��Salir��$
��Left=�$��
10,245,0,0q�$	��Top=�$��75���� -   $	 updateTable��$��
Actualizar��$
��Left=�$��255,245,0,0q�$	��Top=�$��75���� -   
 ��Name$
 headerLabel��$��Label��$
��Left=�$��10,1,0,0q�$	��Top=�$	��385���� -   $	 	notifyBtn��$��	En planta��$
��Left=�$��572,245,0,0q�$	��Top=�$	��214��