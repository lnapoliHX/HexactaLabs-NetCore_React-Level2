import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required("Nombre requerido"),
  costPrice: yup.number().required("Precio costo requerido"),
  salePrice: yup.number().required("Precio de venta requerido"),
  productTypeId: yup.string().required("La categoria es requerida"),
});

export default schema;
