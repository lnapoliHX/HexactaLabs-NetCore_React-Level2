import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string()
            .required('Debe indicar un valor para el Nombre'),
  costPrice: yup.number()
            .lessThan(yup.ref('salePrice'), 'El Costo debe ser menor que el Precio de Venta')
            .required('Debe indicar un valor para el Costo')
            .typeError('El Costo debe ser un valor positivo menor que el Precio de Venta'),
  salePrice: yup.number()
            .positive('El Precio de Venta debe ser un valor positivo')
            .moreThan(yup.ref('costPrice'), 'El Precio de Venta debe ser mayor que el Costo')
            .required('Debe indicar un valor para el Precio de Venta')
            .typeError('El Precio de Venta debe ser un valor positivo mayor que el Costo'),
  stock: yup.number()
            .typeError('El Stock debe ser un valor numérico')
            //.positive('El Stock debe ser un valor positivo')
            .required('Debe indicar un valor para el Stock'),

  productTypeId: yup.string().required(),
  providerId: yup.string().required() 
});

export default schema;
