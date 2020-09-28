import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import SelectField from "../../../../components/inputs/SelectField";
import schema from "../validation";

const ProductForm = props => {
  const { handleSubmit, handleCancel } = props;
  const tipos = [{value: '51f73eff-fb45-48b6-a1b2-075dbea80591', label: 'PT1'}, {value: 'de6b27c1-8b7f-49b5-97e4-70d66ae744bd', label: 'PT2'} ];
  return (
    <Form onSubmit={handleSubmit} className="addForm">
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field label="Precio" name="costPrice" component={InputField} type="text" />
      <Field label="Precio de Venta" name="salePrice" component={InputField} type="text" />
      <Field label="Tipo de Producto" name="productTypeId" component={SelectField} type="select" options={tipos} />
      <Field label="Proveedor" name="providerId" component={InputField} type="text" />
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
