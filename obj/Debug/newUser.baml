   M S B A M L   `   `   `  ����I  EReport Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Report_ManagerReport_Manager  5   	       Report_Manager.newUser   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Report_Manager     5   	     � Title$  Create User��5   	   $��319.39��6   $	��300��6-   
�������$��Center=�6K    � WindowStartupLocation$ CenterScreen=�6o   .��5	      � .+�5
   
   �� -   5         ��Name$ label��5
      $��Nuevo Usuario��6   $
��Left=�67   $��67,0,0,0q�6R   $	��Top=�6d   $��22(�6|   �� 5   
   -   5        $ label1��5      $��Nombre: ��6    $
��Left=�63   $��	47,57,0,0q�6N   $	��Top=�6a   $��26��6y   $��78��6�   �� 5   
   -   5        $ label1_Copy��5      $��Contraseña: ��6%   $
��Left=�6<   $��	47,88,0,0q�6W   $	��Top=�6j   $��26��6�   $��78��6�   �� 5   
   -   5        $ label1_Copy1��5      $��Email:��6&   $
��Left=�67   $��
47,114,0,0q�6R   $	��Top=�6f   $��26��6~   $��78��6�   �� 5   
   -   5         ��Name$ 
newUsrName��5      $
��Left=�6&   $��22��6A   $��
145,61,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��128��6�   $��16(�6�   �� 5   
   -   5        $ 	newUsrPwd��5      $
��Left=�6%   $��22��6@   $��
145,87,0,0q�6L   $
 Wrap=�6`   $	��Top=�6t   $	��128��6�   $��16(�6�   �� 5   
   -   5        $ newUserEmail��5      $
��Left=�6(   $��22��6C   $��145,114,0,0q�6O   $
 Wrap=�6d   $	��Top=�6x   $	��128��6�   $��16(�6�   �� 5   
   -   5         ��Name$ 
createUser��5      $��¡Crear!��6%   $
��Left=�67   $��198,241,0,0q�6R   $	��Top=�6g   $��75��6   $��14(�6�   6�   �� 5   
   -	   5        $ backTo��5      $��Regresar��6!   $
��Left=�64   $��
19,241,0,0q�6O   $	��Top=�6c   $��75��6{   $��14(�6�   6�   �� 5   
   $
��Left=�6   $��84��60   $��
47,145,0,0q�6<   $	��Top=�6P   $	��226��6h   .+�5      	� -
   5         	�Name$ userType��5      $��	Comprador��6,   $��16(�6@   	� 5      -   5        $ impType��5      $��
Importador��6+   $��16(�6@   	� 5      -   5        $ receivedType��5      $��Recibo��60   $��16(�6A   	� 5      -   5        $ managementType��5      $��Gerente��62   $��16(�6D   5      5      5      