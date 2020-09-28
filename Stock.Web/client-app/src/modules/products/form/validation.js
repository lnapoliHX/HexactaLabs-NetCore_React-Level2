import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costPrice: yup.number().positive().min(0).required(),
  salePrice: yup.number().positive().min(0).required(),
 productTypeId : yup.string().required(),
});

export default schema;