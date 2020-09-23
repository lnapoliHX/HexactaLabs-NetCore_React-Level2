import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costPrice: yup.number().positive().min(1),
  salePrice: yup.number().positive().min(1),
  providerId: yup.string().required(),
  productTypeId : yup.string().required(),
});

export default schema;
