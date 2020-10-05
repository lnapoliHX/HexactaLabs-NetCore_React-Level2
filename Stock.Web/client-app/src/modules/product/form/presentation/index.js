import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button} from "reactstrap";
import SelectField from "../../../../components/inputs/SelectField";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";


const ProductForm = props => {
  const { handleSubmit, handleCancel} = props;
  return (
    <Form onSubmit={handleSubmit} className="addForm">
      <Field label="Nombre del producto" name="name" component={InputField} type="text" />
      <Field label="Costo" name="costPrice" component={InputField} type="text" />
      <Field label="Precio de venta" name="salePrice" component={InputField} type="text" />
      
<Field
        name="productTypeId"
        label="Tipo de producto"
        component={SelectField}
        type="select"
        options={props.productTypeOptions}
      />
      <Field
        name="providerId"
        label="Proveedor"
        component={SelectField}
        type="select"
        options={props.providerOptions}
      />

      <Button className="provider-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="provider-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

ProductForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired
};

export default reduxForm({
  form: "product",
  validate: Validator(schema),
  enableReinitialize: true
})(ProductForm);