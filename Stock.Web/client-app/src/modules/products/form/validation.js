import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costPrice: yup.number().min(0).required(),
  salePrice: yup.number().min(0).required(),
  stock: yup.number().min(0).required(),
  productTypeId: yup.string().required(),
  providerId: yup.string().required()
});

export default schema;
