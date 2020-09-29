import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costPrice: yup.number().positive().required(),
  salePrice: yup.number().positive().required(),
  productTypeId: yup.string().required(),
  providerId: yup.string().nullable()
});

export default schema;
