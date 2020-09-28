import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";

const ProductForm = props => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit}>
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field label="Precio de costo" name="costprice"component={InputField} type="text" />
      <Field label="Precio de venta" name="saleprice"component={InputField} type="text" />
      <Field label="Tipo de producto" name="producttype" component={InputField} type="text" />
      <Field label="Proveedor" name="provider" component={InputField} type="text" />
      <Field label="Stock" name="_stock" component={InputField} type="text" />      
      <Button className="product-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="product-form__button"
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
