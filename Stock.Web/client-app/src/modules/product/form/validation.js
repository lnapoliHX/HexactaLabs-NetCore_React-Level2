import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costPrice: yup.string().required(),
  salePrice: yup.string().required(),
  stock: yup.string().required(),
  productTypeId: yup.string(),
  providerId: yup.string()
});

export default schema;
