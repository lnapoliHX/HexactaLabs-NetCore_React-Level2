import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import InputList from "../../../../components/inputs/InputList";
import schema from "../validation";

const ProductForm = props => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit} className="addForm">
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field label="Precio de Costo" name="costPrice" component={InputField} type="text" />
      <Field label="Precio de Venta" name="salePrice" component={InputField} type="text" />
      <InputList URL="/producttype" name="productTypeId" label="Categoría" param="description"/>
      <Field label="Stock" name="stock" component={InputField} type="number" />
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
